using MongoDB.Bson.Serialization.Attributes;
using Notification.Core.Common.CQRS;

namespace Notification.Worker.Domain.Entities;

public class Sent : Entity
{
    public Sent(string externalId, string partnerSystem, bool success)
    {
        if (string.IsNullOrWhiteSpace(externalId))
            throw new ArgumentException(nameof(externalId));

        if (string.IsNullOrWhiteSpace(partnerSystem))
            throw new ArgumentException(nameof(partnerSystem));

        ExternalId = externalId;
        PartnerSystem = partnerSystem;
        Success = success;
    }

    protected Sent()
    {
    }
      
    [BsonElement("ExternalId")]
    public string ExternalId { get; private set; }

    
    [BsonElement("PartnerSystem")]
    public string PartnerSystem { get; private set; }

    [BsonElement("Success")]
    public bool Success { get; private set; }
    
    [BsonElement("SendAt")] 
    public DateTime SendAt { get; private set; } = DateTime.UtcNow;
    
}