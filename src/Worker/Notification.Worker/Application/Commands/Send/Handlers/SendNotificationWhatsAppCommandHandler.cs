using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.Core.Common.Domain;
using Notification.Core.Domain.Enums;
using Notification.Worker.Data.Repositories.Interfaces;
using Notification.Worker.Domain.Entities;
using Notification.Worker.Domain.Services.Interfaces;

namespace Notification.Worker.Application.Commands.Handlers;

public class SendNotificationWhatsAppCommandHandler: IRequestHandler<SendNotificationWhatsAppCommand, IActionResult>
{
    public SendNotificationWhatsAppCommandHandler(INotificationRepository notificationRepository, IWhatsAppServices whatsAppServices)
    {
        _notificationRepository = notificationRepository;
        _whatsAppServices = whatsAppServices;
    }

    private readonly INotificationRepository _notificationRepository;
    private readonly IWhatsAppServices _whatsAppServices;
    
    public async Task<IActionResult> Handle(SendNotificationWhatsAppCommand request, CancellationToken cancellationToken)
    {
        bool create = false;
        Domain.Notification notification = await _notificationRepository.GetByCorrelationId(request.AggregateId);

        if (notification != null && notification.Sent.Any(c => !c.Success))
            throw new DomainException("Already processed");

        if (notification is null)
        {
            create = true;
            notification = Create(request);
        }
        
        await notification.Send(_whatsAppServices);
        
        if(create)
            _notificationRepository.Add(notification);
        else 
            _notificationRepository.Update(notification);
        
        await _notificationRepository.unitOfWork.Commit();

        return new OkResult();
    }
    
    private Domain.Notification Create(SendNotificationWhatsAppCommand request)
    {
        var parameters = request.Params.Select(c => new Parameter(c.Key, c.Value)).ToList();
        return new Domain.Notification(request.AggregateId.ToString(), request.Recipient, ENotificationType.Email , parameters);
    }
    
}