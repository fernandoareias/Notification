using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Notification.Core.Common.CQRS;
using Notification.Core.MessageBus.Configurations;
using Notification.Core.MessageBus.Services.Interfaces;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using Serilog;

namespace Notification.Core.MessageBus.Services;

public class MessageBus : IMessageBus
{
    public MessageBus(IOptions<MessageBusConfigs> config, ILogger<MessageBus> logger)
    {
        _busConfigs = config.Value; 
        _logger = logger; 
    }
    
    private bool _isConnected = false;
    private IConnection _connection;
    private IModel _consumerChannel;
    private ConnectionFactory factory = new ConnectionFactory() { Uri = new Uri("amqp://admin:admin@localhost:5672/") };

    private readonly IDictionary<string, List<string>> _exchangeRoutingKeys = new Dictionary<string, List<string>>();
    private readonly MessageBusConfigs _busConfigs;
    private readonly ILogger<MessageBus> _logger;
    
    private IConnection connection
    {
        get
        {
            if (!_isConnected)
            {
                Connect();
            }

            return _connection;
        }
    }
    
    public void Connect()
    {
        var policy = RetryPolicy.Handle<SocketException>().Or<BrokerUnreachableException>()
            .WaitAndRetry(_busConfigs.RetryCount, op => TimeSpan.FromSeconds(Math.Pow(2, op)), (ex, time) =>
            {
                Console.WriteLine("Couldn't connect to RabbitMQ server...");
            });

        policy.Execute(() =>
        {
            _connection = factory.CreateConnection();
            _isConnected = true;
            Console.WriteLine("connected!");
        });
    }
    
    private void DeclareQueueAndExchange(string routingKey, string exchangeName, IModel channel)
    {
        bool containsExchange = _exchangeRoutingKeys.ContainsKey(exchangeName);
        bool containsRoutingKey = containsExchange && _exchangeRoutingKeys[exchangeName].Contains(routingKey);

        if (containsExchange && containsRoutingKey) return;
         
        if (!containsExchange!)
        {
            // Declaração da exchange
            channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct, durable: true);
            _exchangeRoutingKeys.Add(exchangeName, new List<string>());
        }

        if (!containsRoutingKey)
        {
            // Declaração da fila
            channel.QueueDeclare(queue: routingKey, durable: true, exclusive: false, autoDelete: false, arguments: null);

            // Vinculação da fila à exchange usando a routing key
            channel.QueueBind(queue: routingKey, exchange: exchangeName, routingKey: routingKey);
            _exchangeRoutingKeys[exchangeName].Add(routingKey);
        }
        
    }

    public void Publish(string exchange, string routingKey, dynamic command)
    { 
        using (var channel = connection.CreateModel())
        {
            DeclareQueueAndExchange(routingKey, exchange, channel);
            
            string commandJson = JsonSerializer.Serialize(command);
            _logger.LogInformation($"[PUBLISH] - Exchange: {exchange} | Type: {ExchangeType.Direct.ToString()} | Queue: {routingKey} | RoutingKey: {routingKey} | Message: {commandJson}");

            var body = Encoding.UTF8.GetBytes(commandJson);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            channel.BasicPublish(exchange: exchange, routingKey: routingKey, basicProperties: properties, body: body);
        }
    }

    public void Publish(string exchange, Event @event)
    {
        using(var channel = connection.CreateModel())
        {
            channel.ExchangeDeclare(exchange, type: ExchangeType.Fanout);
        
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish(
                exchange: "logs",
                routingKey: string.Empty,
                basicProperties: null,
                body: body);
        }
    }

    public void Subscribe<TMessage>(string exchange, string routingKey, Action<TMessage> function, CancellationToken stoppingToken)
    {
       // await Task.Yield(); 
       Console.WriteLine("[CONSUMER] - Create model");
       _consumerChannel = connection.CreateModel();
       DeclareQueueAndExchange(routingKey, exchange, _consumerChannel);

       var consumer = new EventingBasicConsumer(_consumerChannel);
       Console.WriteLine("[CONSUMER] - Run basic consumer");
       _consumerChannel.BasicConsume(queue: routingKey, autoAck: false, consumer: consumer);
        
       consumer.Received += (sender, eventArgs) =>
       {
           try
           {
               var messageBody = eventArgs.Body.ToArray();
               var messageJson = Encoding.UTF8.GetString(messageBody);
               var args = JsonSerializer.Deserialize<TMessage>(messageJson);
               Console.WriteLine(
                   $"[SUBSCRIBE] - Exchange: {exchange} | Type: {ExchangeType.Direct.ToString()} | Queue: {routingKey} | RoutingKey: {routingKey} | Message: {messageJson}");

               function(args);

               _consumerChannel.BasicAck(eventArgs.DeliveryTag, false);
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex);
               _consumerChannel.BasicNack(eventArgs.DeliveryTag, false, true);
           }
       };

       
    }

    public void Dispose()
    {
        _connection.Dispose();
        _consumerChannel.Dispose();
    }
}