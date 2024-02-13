using Notification.Worker.Application.Commands.Send.Base;

namespace Notification.Worker.Application.Commands;

public class SendNotificationPushCommand : SendNotificationCommand
{
    public SendNotificationPushCommand(CreateNotificationCommand request) : base(request)
    {
        
    }
}