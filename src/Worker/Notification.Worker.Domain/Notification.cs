using MongoDB.Bson.Serialization.Attributes;
using Notification.Core.Domain.Enums; 
using Notification.Worker.Domain.Entities;
using Notification.Core.Common.CQRS;
using Notification.Worker.Domain.Events.Factories;

namespace Notification.Worker.Domain;

public class Notification : AggregateRoot
{
    public Notification(string correlationId, string recipient, ENotificationType type, List<Parameter>? parameters = null)
    {
        if (string.IsNullOrWhiteSpace(correlationId))
            throw new ArgumentException(nameof(correlationId));

        if (string.IsNullOrWhiteSpace(recipient))
            throw new ArgumentException(nameof(recipient));

        CorrelationId = correlationId;
        Recipient = recipient;
        Type = type;
        Parameters = parameters;
    }

    protected Notification()
    {
        
    }
    
    protected Notification(string correlationId, string recipient, ENotificationType type, List<Parameter> parameters, List<Sent> sent)
    {
        CorrelationId = correlationId;
        Recipient = recipient;
        Type = type;
        Parameters = parameters;
        Sent = sent;
    }

    [BsonElement("CorrelationId")]
    public string CorrelationId { get; private set; }
    
    [BsonElement("Recipient")]
    public string Recipient { get; private set; }
    
    [BsonElement("Type")]
    public ENotificationType Type { get; private set; }
    
    [BsonElement("Parameters")]
    public List<Parameter>? Parameters { get; private set; }

    [BsonElement("Sent")] 
    public List<Sent> Sent { get; private set; } = new List<Sent>();
    
    public async Task Send(IDomainService<Sent, Notification> service)
    {
        var sent =  await service.Process(this);

        if (Sent.Any(c => c.ExternalId == sent.ExternalId))
            throw new InvalidOperationException("Already sent");
        
        if(!sent.Success)
            AddEvent(NotificationDeliveryFailureEventFactory.Create(Type, CorrelationId));
        
        Sent.Add(sent);
    }

}