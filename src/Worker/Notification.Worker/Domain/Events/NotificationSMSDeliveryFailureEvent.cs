using System.Text.Json.Serialization;
using MongoDB.Bson;
using Notification.Worker.Domain.Events.Common;

namespace Notification.Worker.Domain.Events;

public class NotificationSMSDeliveryFailureEvent : NotificationDeliveryFailureEvent
{ 
    public NotificationSMSDeliveryFailureEvent(string correlationId) : base(correlationId, "notifications-failure", "sms-delivery-failure-event")
    {
       
    }
     
}