using Notification.Core.Common.CQRS;

namespace Notification.Core.MessageBus.Services.Interfaces;


 
public interface IMessageBus : IDisposable  
{ 
    void Publish(string exchange, string routingKey, Command command);
    void Publish(string exchange, Event @event);
    void Subscribe<TMessage>(string exchange, string routingKey, Func<TMessage, Task> function, CancellationToken stoppingToken);
}