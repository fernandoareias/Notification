using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.Core.Common.Domain;
using Notification.Worker.Data.Repositories.Interfaces;
using Notification.Worker.Domain.Services.Interfaces;

namespace Notification.Worker.Application.Commands.Handlers;

public class RetrySendNotificationWhatsAppCommandHandler : IRequestHandler<RetrySendNotificationWhatsAppCommand, IActionResult>
{
    public RetrySendNotificationWhatsAppCommandHandler(INotificationRepository notificationRepository, IEmailServices emailServices)
    {
        _notificationRepository = notificationRepository;
        _emailServices = emailServices;
    }

    private readonly INotificationRepository _notificationRepository;
    private readonly IEmailServices _emailServices;
    
    public async Task<IActionResult> Handle(RetrySendNotificationWhatsAppCommand request, CancellationToken cancellationToken)
    {
        Domain.Notification notification = await _notificationRepository.GetByCorrelationId(request.CorrelationId);

        if (notification == null)
            throw new DomainException("Notification not exists.");
            
        await notification.Send(_emailServices);
        
        _notificationRepository.Update(notification);

        await _notificationRepository.unitOfWork.Commit();

        return new OkResult();
    }
}