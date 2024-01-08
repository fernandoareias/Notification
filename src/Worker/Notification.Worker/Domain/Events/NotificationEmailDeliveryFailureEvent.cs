using MongoDB.Bson;
using Notification.Worker.Domain.Events.Common;

namespace Notification.Worker.Domain.Events;

public class NotificationEmailDeliveryFailureEvent : NotificationDeliveryFailureEvent
{
    public NotificationEmailDeliveryFailureEvent(BsonObjectId aggregateId) : base(aggregateId)
    {
        RouterKey = "sms-delivery-failure-event";
    }
    
    
}