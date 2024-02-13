using System.Text.Json.Serialization;
using Notification.Core.Common.CQRS;

namespace Notification.Worker.Domain.Events.Common;


public abstract class NotificationDeliveryFailureEvent : Event
{
    [JsonConstructor]
    protected NotificationDeliveryFailureEvent(string correlationId, string exchange, string routerKey) : base(exchange, routerKey)
    {
        CorrelationId = correlationId;
    }

    public string CorrelationId { get; private set; }
}