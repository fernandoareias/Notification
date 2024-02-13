using Notification.Worker.Application.Commands.Send.Base;

namespace Notification.Worker.Application.Commands;

public class SendNotificationWhatsAppCommand : SendNotificationCommand
{
    public SendNotificationWhatsAppCommand(CreateNotificationCommand request) : base(request)
    {
        
    }
}