using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.Core.Common.Domain;
using Notification.Core.Mediator.Interfaces;
using Notification.Worker.Application.Commands;
using Notification.Worker.Data.Repositories.Interfaces;
using Notification.Worker.Domain.Services.Interfaces;

namespace Notification.Worker.Domain.Events.Handlers;

public class NotificationEmailDeliveryFailureEventHandler : INotificationHandler<NotificationEmailDeliveryFailureEvent>
{
    public NotificationEmailDeliveryFailureEventHandler(IMediatorHandler mediatorHandler)
    {
        _mediatorHandler = mediatorHandler;
    }


    private readonly IMediatorHandler _mediatorHandler;

    
    public async Task Handle(NotificationEmailDeliveryFailureEvent notification, CancellationToken cancellationToken)
    {
        var command = new RetrySendNotificationEmailCommand(notification.CorrelationId.ToString());

        await _mediatorHandler.Send<RetrySendNotificationEmailCommand>(command);
    }
}