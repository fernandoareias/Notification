using Notification.Worker.Domain.Common;
using Notification.Worker.Domain.Entities;
using Notification.Worker.Domain.Enums;
using Notification.Worker.Domain.Services.Interfaces;

namespace Notification.Worker.Domain.Services;

public class SMSNotificationServices : ISMSNotificationServices
{
    private readonly ILogger<SMSNotificationServices> _logger;
    public async Task<Sent> Process(Notification aggregate)
    {
        throw new NotImplementedException();
    }
}