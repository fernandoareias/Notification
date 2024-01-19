using System.Runtime.Serialization;

namespace Notification.Core.Common.CQRS;

[DataContract]
public abstract class Message
{
  
    [DataMember]
    public Guid AggregateId { get; protected set; }  = Guid.NewGuid();

    [DataMember]
    public string Type { get; private set; } = typeof(Message).Name;
    
    [DataMember] 
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
}