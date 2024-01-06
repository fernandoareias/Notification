using System.Runtime.Serialization;
using MongoDB.Bson;
using Notification.Core.Common.CQRS;

namespace Notification.Worker.Domain.Events.Common;

[DataContract]
public class NotificationDeliveryFailureEvent : Event
{
    [DataMember]
    public BsonObjectId AggregateId { get; private set; }
}