using MediatR;

namespace Notification.Worker.Domain.Events.Handlers;

public class NotificationLetterDeliveryFailureEventHandler : INotificationHandler<NotificationLetterDeliveryFailureEvent>
{
    public Task Handle(NotificationLetterDeliveryFailureEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}