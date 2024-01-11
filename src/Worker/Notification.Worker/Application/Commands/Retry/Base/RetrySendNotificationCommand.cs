using System.Runtime.Serialization;
using Notification.Core.Common.CQRS;

namespace Notification.Worker.Application.Commands.Send.Base;

public abstract class RetrySendNotificationCommand  : Command
{
    protected RetrySendNotificationCommand(string correlationId)
    {
        CorrelationId = correlationId;
    }

    [DataMember]
    public string CorrelationId { get; private set; }
}

