using MediatR;

namespace Notification.Worker.Domain.Events.Handlers;

public class NotificationEmailDeliveryFailureEventHandler : INotificationHandler<NotificationEmailDeliveryFailureEvent>
{
    public Task Handle(NotificationEmailDeliveryFailureEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}