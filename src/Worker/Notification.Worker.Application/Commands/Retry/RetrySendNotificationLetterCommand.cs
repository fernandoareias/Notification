using Notification.Worker.Application.Commands.Send.Base;

namespace Notification.Worker.Application.Commands;

public class RetrySendNotificationLetterCommand : RetrySendNotificationCommand
{
    public RetrySendNotificationLetterCommand(string correlationId) : base(correlationId)
    {
        
    }
}