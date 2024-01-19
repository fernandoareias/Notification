using System.Text.Json.Serialization;
using MongoDB.Bson;
using Notification.Worker.Domain.Events.Common;

namespace Notification.Worker.Domain.Events;

public class NotificationLetterDeliveryFailureEvent : NotificationDeliveryFailureEvent
{
     
    public NotificationLetterDeliveryFailureEvent(string correlationId) 
        : base(correlationId, "notifications-failure", "letter-delivery-failure-event")
    {
    }
     
}