using Notification.Core.Common.CQRS;
using Notification.Worker.Application.Commands.Send.Base;

namespace Notification.Worker.Application.Commands;

public class SendNotificationEmailCommand : SendNotificationCommand
{ 
    public SendNotificationEmailCommand(CreateNotificationCommand request) : base(request)
    {
        
    }
     
}