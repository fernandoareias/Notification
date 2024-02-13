using Notification.Core.Common.CQRS;
using Notification.Worker.Domain.Entities;

namespace Notification.Worker.Domain.Services.Base;

public interface INotificationService: IDomainService<Sent, Notification>
{
}