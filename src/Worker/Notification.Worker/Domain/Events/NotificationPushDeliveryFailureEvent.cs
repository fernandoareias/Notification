using MongoDB.Bson;
using Notification.Worker.Domain.Events.Common;

namespace Notification.Worker.Domain.Events;

public class NotificationPushDeliveryFailureEvent : NotificationDeliveryFailureEvent
{
    public NotificationPushDeliveryFailureEvent(BsonObjectId aggregateId) : base(aggregateId)
    {
        RouterKey = "push-delivery-failure-event";
    }
    
    
}