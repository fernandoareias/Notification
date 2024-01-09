using MongoDB.Bson;
using Notification.Worker.Domain.Events.Common;

namespace Notification.Worker.Domain.Events;

public class NotificationWhatsAppDeliveryFailureEvent : NotificationDeliveryFailureEvent
{
    public NotificationWhatsAppDeliveryFailureEvent(BsonObjectId aggregateId) : base(aggregateId)
    {
        RouterKey = "whatsapp-delivery-failure-event";
    }
    
    
}