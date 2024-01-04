using MediatR;

namespace Notification.Core.Common.CQRS;

public abstract class Event : Message, INotification
{
    
}