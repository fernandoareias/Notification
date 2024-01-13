using Microsoft.AspNetCore.Mvc;
using Notification.Core.Common.CQRS;

namespace Notification.Core.MessageBus.Services.Interfaces;


 
public interface IMessageBus : IDisposable  
{ 
    void Publish(string exchange, string routingKey, dynamic command);
    void Subscribe<TMessage>(string exchange, string routingKey, Func<TMessage, Task> function, CancellationToken stoppingToken);
}