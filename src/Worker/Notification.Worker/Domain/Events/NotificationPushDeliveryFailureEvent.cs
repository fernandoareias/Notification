using MongoDB.Bson;
using Notification.Worker.Domain.Events.Common;

namespace Notification.Worker.Domain.Events;

public class NotificationPushDeliveryFailureEvent : NotificationDeliveryFailureEvent
{
    protected NotificationPushDeliveryFailureEvent()
    {
        
    }
    
    public NotificationPushDeliveryFailureEvent(string aggregateId) : base(aggregateId)
    {
        Exchange = "notifications-failure";
        RouterKey = "push-delivery-failure-event";
    }
    
    
}