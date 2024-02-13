namespace Notification.Worker.Data.Repositories.Interfaces;

public interface INotificationRepository : IRepository<Domain.Notification>
{
    Task<Domain.Notification> GetByCorrelationId(string correlationId);
}