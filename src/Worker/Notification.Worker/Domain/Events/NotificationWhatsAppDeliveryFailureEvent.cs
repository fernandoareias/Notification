using MongoDB.Bson;
using Notification.Worker.Domain.Events.Common;

namespace Notification.Worker.Domain.Events;

public class NotificationWhatsAppDeliveryFailureEvent : NotificationDeliveryFailureEvent
{
    protected NotificationWhatsAppDeliveryFailureEvent()
    {
        
    }
    
    public NotificationWhatsAppDeliveryFailureEvent(string aggregateId) : base(aggregateId)
    {
        Exchange = "notifications-failure";
        RouterKey = "whatsapp-delivery-failure-event";
    }
    
    
}