using Notification.Core.Common.CQRS;
using Notification.Core.Domain.Enums;
using Notification.Worker.Application.Commands.Send.Base;

namespace Notification.Worker.Application.Commands.Factories;

public static class SendNotificationFactory
{
    
    private static Dictionary<ENotificationType, Type> command = new Dictionary<ENotificationType, Type>()
    {
        { ENotificationType.SMS, typeof(SendNotificationSMSCommand)},
        { ENotificationType.Email, typeof(SendNotificationEmailCommand)},
        { ENotificationType.Letter, typeof(SendNotificationLetterCommand)},
        { ENotificationType.Push, typeof(SendNotificationPushCommand)},
        { ENotificationType.WhatsApp, typeof(SendNotificationWhatsAppCommand)},
    };

    public static SendNotificationCommand Create(CreateNotificationCommand request)
    {
        if (!command.ContainsKey(request.Type)) throw new NotSupportedException("Type not found");
        
        var commandType = command[request.Type];
        
        
        return (SendNotificationCommand)Activator.CreateInstance(commandType, request)!;
    }
}