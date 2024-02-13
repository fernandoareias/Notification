using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.Core.Common.CQRS;
using Notification.Core.Common.Domain;
using Notification.Worker.Data.Repositories.Interfaces;
using Notification.Worker.Domain.Services.Interfaces;

namespace Notification.Worker.Application.Commands.Handlers;

public class RetrySendNotificationEmailCommandHandler : IRequestHandler<RetrySendNotificationEmailCommand, View>
{
    public RetrySendNotificationEmailCommandHandler(INotificationRepository notificationRepository, IEmailServices emailServices)
    {
        _notificationRepository = notificationRepository;
        _emailServices = emailServices;
    }

    private readonly INotificationRepository _notificationRepository;
    private readonly IEmailServices _emailServices;
    
    public async Task<View> Handle(RetrySendNotificationEmailCommand request, CancellationToken cancellationToken)
    {
        var notification = await _notificationRepository.GetByCorrelationId(request.CorrelationId);

        if (notification == null)
            throw new DomainException("Already processed");

        await notification.Send(_emailServices);
        
        _notificationRepository.Update(notification);

        await _notificationRepository.unitOfWork.Commit();

        return new OperationView(true);
    }
}