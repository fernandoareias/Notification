using MongoDB.Bson;
using Notification.Worker.Domain.Events.Common;

namespace Notification.Worker.Domain.Events;

public class NotificationSMSDeliveryFailureEvent : NotificationDeliveryFailureEvent
{
    protected NotificationSMSDeliveryFailureEvent()
    {
        
    }
    
    public NotificationSMSDeliveryFailureEvent(string aggregateId) : base(aggregateId)
    {
        Exchange = "notifications-failure";
        RouterKey = "sms-delivery-failure-event";
    }
    
    
}