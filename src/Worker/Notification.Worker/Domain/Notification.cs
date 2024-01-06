using MongoDB.Bson.Serialization.Attributes;
using Notification.Core.Domain.Enums; 
using Notification.Worker.Domain.Entities;
using Notification.Core.Common.CQRS;

namespace Notification.Worker.Domain;

public class Notification : AggregateRoot
{
    public Notification(Guid correlationId, string recipient, ENotificationType type, List<Parameter> parameters)
    {
        CorrelationId = correlationId;
        Recipient = recipient;
        Type = type;
        Parameters = parameters;
    }

    protected Notification()
    {
        
    }

    [BsonElement("Recipient")]
    public Guid CorrelationId { get; private set; }
    
    [BsonElement("Recipient")]
    public string Recipient { get; private set; }
    
    [BsonElement("Type")]
    public ENotificationType Type { get; private set; }
    
    [BsonElement("Parameters")]
    public List<Parameter> Parameters { get; private set; }

    [BsonElement("Sent")] 
    public List<Sent> Sent { get; private set; }
    
    public async Task Send(IDomainService<Sent, Notification> service)
    {
        var sent =  await service.Process(this);
    }

}