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
    private IModel consumerChannel;
    private ConnectionFactory factory = new ConnectionFactory() { Uri = new Uri("amqp://admin:admin@localhost:5672/") };

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

    public void Publish(string exchange, string routingKey, Command command)
    {
        using (var channel = connection.CreateModel())
        {
            channel.ExchangeDeclare(exchange, type: ExchangeType.Direct);
            channel.QueueDeclare(queue: routingKey,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(command));
            channel.BasicPublish(exchange: exchange, routingKey: routingKey, basicProperties: null, body: body);
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

    public async void Subscribe<TMessage>(string exchange, string routingKey, Func<TMessage, Task> function, CancellationToken stoppingToken)
    {
        await Task.Yield();
        
        consumerChannel = connection.CreateModel();
        consumerChannel.ExchangeDeclare(exchange, ExchangeType.Direct); 

        var queue = consumerChannel.QueueDeclare(queue: routingKey);

        var consumer = new EventingBasicConsumer(consumerChannel);

        consumerChannel.BasicConsume(queue: routingKey, autoAck: true, consumer: consumer);

        consumer.Received += async (model, ea) => {
            var messageBody = ea.Body.ToArray();
            var args = JsonSerializer.Deserialize<TMessage>(Encoding.UTF8.GetString(messageBody)); 
            await function(args);
        };
    }

    public void Dispose()
    {
        _connection.Dispose();
    }
}