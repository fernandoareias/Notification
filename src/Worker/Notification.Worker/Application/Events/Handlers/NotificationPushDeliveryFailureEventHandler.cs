using MediatR;

namespace Notification.Worker.Domain.Events.Handlers;

public class NotificationPushDeliveryFailureEventHandler : INotificationHandler<NotificationPushDeliveryFailureEvent>
{
    public Task Handle(NotificationPushDeliveryFailureEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}