using MongoDB.Bson;
using Notification.Worker.Domain.Events.Common;

namespace Notification.Worker.Domain.Events;

public class NotificationLetterDeliveryFailureEvent : NotificationDeliveryFailureEvent
{
    protected NotificationLetterDeliveryFailureEvent()
    {
        
    }
    public NotificationLetterDeliveryFailureEvent(string aggregateId) : base(aggregateId)
    {
        Exchange = "notifications-failure";
        RouterKey = "letter-delivery-failure-event";
    }
    
    
}