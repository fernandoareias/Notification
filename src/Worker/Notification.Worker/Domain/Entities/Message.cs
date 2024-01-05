using MongoDB.Bson.Serialization.Attributes;
using Notification.Worker.Domain.Common;

namespace Notification.Worker.Domain.Entities;

public class Message : Entity
{
    [BsonElement("Layout")]
    public string Layout { get; private set; }
}