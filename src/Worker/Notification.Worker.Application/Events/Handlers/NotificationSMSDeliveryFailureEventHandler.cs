using MediatR;
using Notification.Core.Mediator.Interfaces;
using Notification.Worker.Application.Commands;

namespace Notification.Worker.Domain.Events.Handlers;

public class NotificationSMSDeliveryFailureEventHandler : INotificationHandler<NotificationSMSDeliveryFailureEvent>
{
    public NotificationSMSDeliveryFailureEventHandler(IMediatorHandler mediatorHandler)
    {
        _mediatorHandler = mediatorHandler;
    }


    private readonly IMediatorHandler _mediatorHandler;
    
    public async Task Handle(NotificationSMSDeliveryFailureEvent notification, CancellationToken cancellationToken)
    {
        var command = new RetrySendNotificationSMSCommand(notification.CorrelationId.ToString());

        await _mediatorHandler.Send<RetrySendNotificationSMSCommand>(command);
    }
}