using Notification.Worker.Application.Commands.Send.Base;

namespace Notification.Worker.Application.Commands;

public class RetrySendNotificationPushCommand : RetrySendNotificationCommand
{
    public RetrySendNotificationPushCommand(string correlationId) : base(correlationId)
    {
        
    }
}