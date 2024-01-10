using MongoDB.Bson.Serialization.Attributes;
using Notification.Core.Domain.Enums; 
using Notification.Worker.Domain.Entities;
using Notification.Core.Common.CQRS;
using Notification.Worker.Domain.Events.Factories;

namespace Notification.Worker.Domain;

public class Notification : AggregateRoot
{
    public Notification(string correlationId, string recipient, ENotificationType type, List<Parameter> parameters)
    {
        CorrelationId = correlationId;
        Recipient = recipient;
        Type = type;
        Parameters = parameters;
    }

    protected Notification()
    {
        
    }

    [BsonElement("CorrelationId")]
    public string CorrelationId { get; private set; }
    
    [BsonElement("Recipient")]
    public string Recipient { get; private set; }
    
    [BsonElement("Type")]
    public ENotificationType Type { get; private set; }
    
    [BsonElement("Parameters")]
    public List<Parameter> Parameters { get; private set; }

    private List<Sent> _sents = new List<Sent>();

    [BsonElement("Sent")] 
    public IReadOnlyCollection<Sent> Sent => _sents;
    
    public async Task Send(IDomainService<Sent, Notification> service)
    {
        var sent =  await service.Process(this);

        if (_sents.Any(c => c.ExternalId == sent.ExternalId))
            throw new InvalidOperationException("Already sent");
        
        if(!sent.Success)
            AddEvent(NotificationDeliveryFailureEventFactory.Create(Type, CorrelationId));
        
        _sents.Add(sent);
    }

}