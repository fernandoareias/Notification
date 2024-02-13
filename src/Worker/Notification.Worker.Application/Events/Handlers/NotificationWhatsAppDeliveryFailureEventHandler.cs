using MediatR;
using Notification.Core.Mediator.Interfaces;
using Notification.Worker.Application.Commands;

namespace Notification.Worker.Domain.Events.Handlers;

public class NotificationWhatsAppDeliveryFailureEventHandler : INotificationHandler<NotificationWhatsAppDeliveryFailureEvent>
{
    public NotificationWhatsAppDeliveryFailureEventHandler(IMediatorHandler mediatorHandler)
    {
        _mediatorHandler = mediatorHandler;
    }


    private readonly IMediatorHandler _mediatorHandler;
    
    public async Task Handle(NotificationWhatsAppDeliveryFailureEvent notification, CancellationToken cancellationToken)
    {
        var command = new RetrySendNotificationWhatsAppCommand(notification.CorrelationId.ToString());

        await _mediatorHandler.Send<RetrySendNotificationWhatsAppCommand>(command);
    }
}