using System.Text.Json.Serialization;
using MediatR;

namespace Notification.Core.Common.CQRS;

public abstract class Event : Message, INotification
{
    [JsonIgnore]
    public string Exchange { get; protected set; }
    [JsonIgnore]
    public string RouterKey { get; protected set; }
}