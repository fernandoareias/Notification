using Notification.Worker.Application.Commands.Send.Base;

namespace Notification.Worker.Application.Commands;

public class SendNotificationLetterCommand : SendNotificationCommand
{
    public SendNotificationLetterCommand(CreateNotificationCommand request) : base(request)
    {
        
    }
}