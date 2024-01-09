using MongoDB.Bson;
using Notification.Worker.Domain.Events.Common;

namespace Notification.Worker.Domain.Events;

public class NotificationLetterDeliveryFailureEvent : NotificationDeliveryFailureEvent
{
    public NotificationLetterDeliveryFailureEvent(BsonObjectId aggregateId) : base(aggregateId)
    {
        RouterKey = "letter-delivery-failure-event";
    }
    
    
}