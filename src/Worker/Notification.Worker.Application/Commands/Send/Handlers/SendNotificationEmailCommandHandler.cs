using MediatR;
using Notification.Core.Common.CQRS;
using Notification.Core.Common.Domain;
using Notification.Core.Domain.Enums;
using Notification.Worker.Data.Repositories.Interfaces;
using Notification.Worker.Domain.Entities;
using Notification.Worker.Domain.Services.Interfaces;

namespace Notification.Worker.Application.Commands.Handlers;

public class SendNotificationEmailCommandHandler : IRequestHandler<SendNotificationEmailCommand, View>
{
    public SendNotificationEmailCommandHandler(INotificationRepository notificationRepository, IEmailServices emailServices)
    {
        _notificationRepository = notificationRepository;
        _emailServices = emailServices;
    }

    private readonly INotificationRepository _notificationRepository;
    private readonly IEmailServices _emailServices;
    
    public async Task<View> Handle(SendNotificationEmailCommand request, CancellationToken cancellationToken)
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
        
        await notification.Send(_emailServices);
        
        if(create)
            _notificationRepository.Add(notification);
        else 
            _notificationRepository.Update(notification);

        await _notificationRepository.unitOfWork.Commit();

        return new OperationView(true);
    }

    private Domain.Notification Create(SendNotificationEmailCommand request)
    {
        var parameters = request.Params.Select(c => new Parameter(c.Key, c.Value)).ToList();
        return new Domain.Notification(request.AggregateId.ToString(), request.Recipient, ENotificationType.Email , parameters);
    }
}