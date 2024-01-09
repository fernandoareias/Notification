using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.Worker.Data.Repositories.Interfaces;
using Notification.Worker.Domain.Services.Interfaces;

namespace Notification.Worker.Application.Commands.Handlers;

public class SendNotificationSMSCommandHandler : IRequestHandler<SendNotificationSMSCommand, IActionResult>
{
    public SendNotificationSMSCommandHandler(INotificationRepository notificationRepository, ISMSServices smsServices)
    {
        _notificationRepository = notificationRepository;
        _smsServices = smsServices;
    }

    private readonly INotificationRepository _notificationRepository;
    private readonly ISMSServices _smsServices;
    public Task<IActionResult> Handle(SendNotificationSMSCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}