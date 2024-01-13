using MediatR;

namespace Notification.Worker.Domain.Events.Handlers;

public class NotificationWhatsAppDeliveryFailureEventHandler : INotificationHandler<NotificationWhatsAppDeliveryFailureEvent>
{
    public Task Handle(NotificationWhatsAppDeliveryFailureEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}