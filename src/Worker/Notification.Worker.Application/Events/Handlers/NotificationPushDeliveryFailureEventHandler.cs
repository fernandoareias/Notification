using MediatR;
using Notification.Core.Mediator.Interfaces;
using Notification.Worker.Application.Commands;

namespace Notification.Worker.Domain.Events.Handlers;

public class NotificationPushDeliveryFailureEventHandler : INotificationHandler<NotificationPushDeliveryFailureEvent>
{
    public NotificationPushDeliveryFailureEventHandler(IMediatorHandler mediatorHandler)
    {
        _mediatorHandler = mediatorHandler;
    }


    private readonly IMediatorHandler _mediatorHandler;
    
    public async Task Handle(NotificationPushDeliveryFailureEvent notification, CancellationToken cancellationToken)
    {
        var command = new RetrySendNotificationPushCommand(notification.CorrelationId.ToString());

        await _mediatorHandler.Send<RetrySendNotificationPushCommand>(command);
    }
}