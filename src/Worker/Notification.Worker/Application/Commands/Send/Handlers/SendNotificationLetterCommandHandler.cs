using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.Core.Common.Domain;
using Notification.Core.Domain.Enums;
using Notification.Worker.Data.Repositories.Interfaces;
using Notification.Worker.Domain.Entities;
using Notification.Worker.Domain.Services.Interfaces;

namespace Notification.Worker.Application.Commands.Handlers;

public class SendNotificationLetterCommandHandler : IRequestHandler<SendNotificationLetterCommand, IActionResult>
{
    public SendNotificationLetterCommandHandler(INotificationRepository notificationRepository, ILetterServices letterServices)
    {
        _notificationRepository = notificationRepository;
        _letterServices = letterServices;
    }

    private readonly INotificationRepository _notificationRepository;
    private readonly ILetterServices _letterServices;
    
    public async Task<IActionResult> Handle(SendNotificationLetterCommand request, CancellationToken cancellationToken)
    {
        bool create = false;
        Domain.Notification notification = await _notificationRepository.GetByCorrelationId(request.AggregateId.ToString());

        if (notification != null && notification.Sent.Any(c => !c.Success))
            throw new DomainException("Already processed");
            
        if (notification is null)
        {
            create = true;
            notification = Create(request);
        }
        
        await notification.Send(_letterServices);
        
        if(create)
            _notificationRepository.Add(notification);
        else 
            _notificationRepository.Update(notification);

        await _notificationRepository.unitOfWork.Commit();
        
        return new OkResult();
    }
    
    private Domain.Notification Create(SendNotificationLetterCommand request)
    {
        var parameters = request.Params.Select(c => new Parameter(c.Key, c.Value)).ToList();
        return new Domain.Notification(request.AggregateId.ToString(), request.Recipient, ENotificationType.Email , parameters);
    }
}