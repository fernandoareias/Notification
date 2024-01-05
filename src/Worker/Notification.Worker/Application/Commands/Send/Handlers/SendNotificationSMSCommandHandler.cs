using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Notification.Worker.Application.Commands.Handlers;

public class SendNotificationSMSCommandHandler : IRequestHandler<SendNotificationSMSCommand, IActionResult>
{
    public Task<IActionResult> Handle(SendNotificationSMSCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}