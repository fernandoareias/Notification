using System.Text.Json.Serialization;
using MongoDB.Bson;
using Notification.Worker.Domain.Events.Common;

namespace Notification.Worker.Domain.Events;

public class NotificationEmailDeliveryFailureEvent : NotificationDeliveryFailureEvent
{ 
    public NotificationEmailDeliveryFailureEvent(string correlationId) : base(correlationId, "notifications-failure", "email-delivery-failure-event")
    { 
    }
     
}