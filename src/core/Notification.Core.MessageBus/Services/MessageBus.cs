using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
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

    private readonly IDictionary<string, string> _exchange = new Dictionary<string, string>();
    private readonly IDictionary<string, string> _routingKeys = new Dictionary<string, string>();
    
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
        });
    }
    
    private void DeclareQueueAndExchange(string routingKey, string exchangeName, IModel channel)
    {
        bool containsExchange = _exchange.ContainsKey(exchangeName);
        bool containsRoutingKey = containsExchange && _routingKeys.ContainsKey(routingKey);

        if (containsExchange && containsRoutingKey) return;
         
        if (!containsExchange!)
        {
            // Declaração da exchange
            channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct, durable: true);
            _exchange.Add(exchangeName, exchangeName);
        }

        if (!containsRoutingKey)
        {
            // Declaração da fila
            channel.QueueDeclare(queue: routingKey, durable: true, exclusive: false, autoDelete: false, arguments: null);

            // Vinculação da fila à exchange usando a routing key
            channel.QueueBind(queue: routingKey, exchange: exchangeName, routingKey: routingKey);
            _routingKeys.Add(routingKey, routingKey);
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
            properties.Headers = new Dictionary<string, object>
            {
                { "X-Retry-Count", 0 } 
            };

            channel.BasicPublish(exchange: exchange, routingKey: routingKey, basicProperties: properties, body: body);
        }
    }

    public void Subscribe<TMessage>(string exchange, string routingKey, Func<TMessage, Task> function, CancellationToken stoppingToken)
    {
       // await Task.Yield(); 
       _consumerChannel = connection.CreateModel();
       DeclareQueueAndExchange(routingKey, exchange, _consumerChannel);

       var consumer = new EventingBasicConsumer(_consumerChannel);
       _consumerChannel.BasicConsume(queue: routingKey, autoAck: false, consumer: consumer);
        
       consumer.Received += async (sender, eventArgs) =>
       {
           try
           {
               var messageBody = eventArgs.Body.ToArray();
               var messageJson = Encoding.UTF8.GetString(messageBody);
               var args = JsonSerializer.Deserialize<TMessage>(messageJson);
               Console.WriteLine(
                   $"[SUBSCRIBE] - Exchange: {exchange} | Type: {ExchangeType.Direct.ToString()} | Queue: {routingKey} | RoutingKey: {routingKey} | Message: {messageJson}");

               await function(args);

               _consumerChannel.BasicAck(eventArgs.DeliveryTag, false);
           }
           catch (Exception ex)
           {
               // int retryCount = GetRetryCount(eventArgs.BasicProperties);
               // if (retryCount >= 5)
               //     PublishDLX<TMessage>(eventArgs,$"{exchange}-failure", $"{routingKey}-failure");
               //
               Console.WriteLine($"[SUBSCRIBE][EXCEPTION] - Exchange: {exchange} | Queue: {routingKey} | RoutingKey: {routingKey} | Exception {ex.Message}");
               // eventArgs.BasicProperties.Headers["X-Retry-Count"] = retryCount + 1;
               _consumerChannel.BasicNack(eventArgs.DeliveryTag, false, true);
           }
       };

       
    }

    // private void PublishDLX<TMessage>(BasicDeliverEventArgs eventArgs, string exchange, string routingKey)
    // {
    //     var messageBody = eventArgs.Body.ToArray();
    //     var messageJson = Encoding.UTF8.GetString(messageBody);
    //     var args = JsonSerializer.Deserialize<TMessage>(messageJson);
    //     
    //     Publish($"{exchange}-failure", $"{routingKey}-failure", args);
    //     
    // }

    // private int GetRetryCount(IBasicProperties properties)
    // {
    //     if (properties.Headers != null && properties.Headers.TryGetValue("X-Retry-Count", out var retryCountObj) && retryCountObj is int retryCount)
    //     {
    //         return retryCount;
    //     }
    //
    //     return 0;
    // }
    
    public void Dispose()
    {
        _connection?.Dispose();
        _consumerChannel?.Dispose();
    }
}