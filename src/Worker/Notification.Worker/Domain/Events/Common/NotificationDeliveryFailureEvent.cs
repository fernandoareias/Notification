using System.Runtime.Serialization;
using MongoDB.Bson;
using Notification.Core.Common.CQRS;

namespace Notification.Worker.Domain.Events.Common;

[DataContract]
public abstract class NotificationDeliveryFailureEvent : Event
{
    protected NotificationDeliveryFailureEvent()
    {
        
    }
    protected NotificationDeliveryFailureEvent(string correlationId)
    {
        CorrelationId = correlationId;
    }

    [DataMember]
    public string CorrelationId { get; private set; }
}