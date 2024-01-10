using MongoDB.Driver;
using Notification.Worker.Data.Interfaces;
using Notification.Worker.Data.Repositories.Interfaces;

namespace Notification.Worker.Data.Repositories;

public class NotificationRepository : BaseRepository<Domain.Notification>, INotificationRepository 
{
    public NotificationRepository(IMongoContext context) : base(context)
    {
    }

    public async Task<Domain.Notification> GetByCorrelationId(Guid correlationId)
    {
        var data = await DbSet.FindAsync(Builders<Domain.Notification>.Filter.Eq("CorrelationId", correlationId.ToString()));
        return data.SingleOrDefault();
    }
}