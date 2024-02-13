using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.API.Application.Commands.Views;
using Notification.Core.Common.CQRS;
using Notification.Core.MessageBus.Services.Interfaces;

namespace Notification.API.Application.Commands.Handlers;

public class CreateNotificationCommandHandlers : IRequestHandler<CreateNotificationCommand, View>
{
    private readonly IMessageBus _messageBus;

    public CreateNotificationCommandHandlers(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    public async Task<View> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        _messageBus.Publish("notifications", "send-notification-" + request.Type.ToString(), request);

        return new CreateNotificationCommandView(request);
    }
}