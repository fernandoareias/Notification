using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Notification.Worker.Application.Commands.Handlers;

public class SendNotificationEmailCommandHandler : IRequestHandler<SendNotificationEmailCommand, IActionResult>
{
    public Task<IActionResult> Handle(SendNotificationEmailCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}