using System.Runtime.Serialization; 
using Notification.Core.Common.CQRS;
using Notification.Core.Domain.Enums;
using Notification.Worker.Application.Commands.Send.Base;

namespace Notification.Worker.Application.Commands;

public class SendNotificationSMSCommand : SendNotificationCommand
{ 
    public SendNotificationSMSCommand(CreateNotificationCommand request) : base(request)
    {
        
    }
     
}

 