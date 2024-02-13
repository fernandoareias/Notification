using Notification.Worker.Application.Commands.Send.Base;

namespace Notification.Worker.Application.Commands;

public class RetrySendNotificationWhatsAppCommand : RetrySendNotificationCommand
{
    public RetrySendNotificationWhatsAppCommand(string correlationId) : base(correlationId)
    {
        
    }
}