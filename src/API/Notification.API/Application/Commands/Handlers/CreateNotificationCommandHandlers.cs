using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.API.Application.Commands.Views;
using Notification.Core.MessageBus.Services.Interfaces;

namespace Notification.API.Application.Commands.Handlers;

public class CreateNotificationCommandHandlers : IRequestHandler<CreateNotificationCommand, IActionResult>
{
    private readonly IMessageBus _messageBus;

    public CreateNotificationCommandHandlers(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    public async Task<IActionResult> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        _messageBus.Publish("notifications", "send-notification-" + request.Type.ToString(), request);

        return new CreatedResult(request.AggregateId.ToString(), new CreateNotificationCommandView(request));
    }
}