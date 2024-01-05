using Notification.Core.Common.CQRS;

namespace Notification.Core.MessageBus.Services.Interfaces;


 
public interface IMessageBus : IDisposable  
{ 
    void Publish(string exchange, string routingKey, dynamic command);
    void Publish(string exchange, Event @event);
    void Subscribe<TMessage>(string exchange, string routingKey, Action<TMessage> function, CancellationToken stoppingToken);
}