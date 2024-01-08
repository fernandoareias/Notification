using MongoDB.Bson.Serialization.Attributes;
using Notification.Worker.Domain.Common;

namespace Notification.Worker.Domain.Entities;

public class Parameter : ValueObjects
{
    public Parameter(string key, string value)
    {
        Key = key;
        Value = value;
    }

    protected Parameter()
    {
        
    }
    
    
    [BsonElement("Key")]
    public string Key { get; private set; }
    
    [BsonElement("Value")]
    public string Value { get; private set; }
}