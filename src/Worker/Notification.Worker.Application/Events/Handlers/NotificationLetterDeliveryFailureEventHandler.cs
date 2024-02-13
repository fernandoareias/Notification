using MediatR;
using Notification.Core.Mediator.Interfaces;
using Notification.Worker.Application.Commands;

namespace Notification.Worker.Domain.Events.Handlers;

public class NotificationLetterDeliveryFailureEventHandler : INotificationHandler<NotificationLetterDeliveryFailureEvent>
{
    public NotificationLetterDeliveryFailureEventHandler(IMediatorHandler mediatorHandler)
    {
        _mediatorHandler = mediatorHandler;
    }


    private readonly IMediatorHandler _mediatorHandler;

    
    public async Task Handle(NotificationLetterDeliveryFailureEvent notification, CancellationToken cancellationToken)
    {
        var command = new RetrySendNotificationLetterCommand(notification.CorrelationId.ToString());

        await _mediatorHandler.Send<RetrySendNotificationLetterCommand>(command);
    }
}