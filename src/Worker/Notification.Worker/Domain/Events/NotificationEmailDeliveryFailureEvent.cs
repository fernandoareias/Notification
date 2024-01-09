using MongoDB.Bson;
using Notification.Worker.Domain.Events.Common;

namespace Notification.Worker.Domain.Events;

public class NotificationEmailDeliveryFailureEvent : NotificationDeliveryFailureEvent
{
    public NotificationEmailDeliveryFailureEvent(BsonObjectId aggregateId) : base(aggregateId)
    {
        RouterKey = "email-delivery-failure-event";
    }
    
    
}