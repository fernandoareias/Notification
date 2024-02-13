using System.Runtime.Serialization; 
using Notification.Core.Common.CQRS;
using Notification.Core.Domain.Enums;
using Notification.Worker.Application.Commands.Send.Base;

namespace Notification.Worker.Application.Commands;

public class RetrySendNotificationSMSCommand : RetrySendNotificationCommand
{ 
    public RetrySendNotificationSMSCommand(string correlationId) : base(correlationId)
    {
        
    }
     
}

 