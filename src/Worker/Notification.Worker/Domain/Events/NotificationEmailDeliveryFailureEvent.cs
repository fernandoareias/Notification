using MongoDB.Bson;
using Notification.Worker.Domain.Events.Common;

namespace Notification.Worker.Domain.Events;

public class NotificationEmailDeliveryFailureEvent : NotificationDeliveryFailureEvent
{
    protected NotificationEmailDeliveryFailureEvent()
    {
        
    }
    public NotificationEmailDeliveryFailureEvent(string aggregateId) : base(aggregateId)
    {
        Exchange = "notifications-failure";
        RouterKey = "email-delivery-failure-event";
    }
    
    
}