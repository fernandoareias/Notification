using MediatR;

namespace Notification.Worker.Domain.Events.Handlers;

public class NotificationSMSDeliveryFailureEventHandler : INotificationHandler<NotificationSMSDeliveryFailureEvent>
{
    public Task Handle(NotificationSMSDeliveryFailureEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}