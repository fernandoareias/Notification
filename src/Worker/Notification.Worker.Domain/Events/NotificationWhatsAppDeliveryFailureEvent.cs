using System.Text.Json.Serialization;
using MongoDB.Bson;
using Notification.Worker.Domain.Events.Common;

namespace Notification.Worker.Domain.Events;

public class NotificationWhatsAppDeliveryFailureEvent : NotificationDeliveryFailureEvent
{ 
    public NotificationWhatsAppDeliveryFailureEvent(string correlationId) : base(correlationId, "notifications-failure", "whatsapp-delivery-failure-event")
    {
       
    }
     
}