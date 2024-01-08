using System.Runtime.Serialization;
using MongoDB.Bson;
using Notification.Core.Common.CQRS;

namespace Notification.Worker.Domain.Events.Common;

[DataContract]
public abstract class NotificationDeliveryFailureEvent : Event
{
    protected NotificationDeliveryFailureEvent(BsonObjectId aggregateId)
    {
        AggregateId = aggregateId;
    }

    [DataMember]
    public BsonObjectId AggregateId { get; private set; }
}