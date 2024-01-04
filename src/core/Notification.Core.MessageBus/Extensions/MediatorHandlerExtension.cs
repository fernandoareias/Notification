using Notification.Core.Common.CQRS;
using Notification.Core.Mediator.Interfaces;
using Notification.Core.MessageBus.Services.Interfaces;

namespace Notification.Core.MessageBus.Extensions;

public static class MediatorHandlerExtension
{
   
    public static Task Publish<TMessage>(this IMediatorHandler handler,TMessage query) where TMessage : Message
    {
        throw new NotImplementedException();
    }
}