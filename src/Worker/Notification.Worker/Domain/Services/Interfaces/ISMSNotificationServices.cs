using Notification.Core.Common.CQRS;
using Notification.Worker.Domain.Entities;
using Notification.Worker.Domain.Enums;

namespace Notification.Worker.Domain.Services.Interfaces;

public interface ISMSNotificationServices : IDomainService<Sent, Notification>
{
    
}