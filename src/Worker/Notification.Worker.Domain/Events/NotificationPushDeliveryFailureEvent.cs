using Notification.Worker.Domain.Events.Common;

namespace Notification.Worker.Domain.Events;

public class NotificationPushDeliveryFailureEvent : NotificationDeliveryFailureEvent
{  
    public NotificationPushDeliveryFailureEvent(string correlationId) : base(correlationId, "notifications-failure", "push-delivery-failure-event")
    {
      
    }
     
}