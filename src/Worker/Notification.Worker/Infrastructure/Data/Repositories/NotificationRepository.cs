using Notification.Worker.Data.Interfaces;
using Notification.Worker.Data.Repositories.Interfaces;

namespace Notification.Worker.Data.Repositories;

public class NotificationRepository : BaseRepository<Domain.Notification>, INotificationRepository 
{
    public NotificationRepository(IMongoContext context) : base(context)
    {
    }
}