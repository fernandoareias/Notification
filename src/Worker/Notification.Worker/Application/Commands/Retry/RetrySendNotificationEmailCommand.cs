using Notification.Core.Common.CQRS;
using Notification.Worker.Application.Commands.Send.Base;

namespace Notification.Worker.Application.Commands;

public class RetrySendNotificationEmailCommand : RetrySendNotificationCommand
{ 
    public RetrySendNotificationEmailCommand(string correlationId) : base(correlationId)
    {
        
    }
     
}