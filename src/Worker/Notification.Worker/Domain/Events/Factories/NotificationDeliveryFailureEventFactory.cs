using MongoDB.Bson;
using Notification.Core.Domain.Enums;
using Notification.Worker.Domain.Events.Common;

namespace Notification.Worker.Domain.Events.Factories;

public static class NotificationDeliveryFailureEventFactory
{
    public static NotificationDeliveryFailureEvent Create(ENotificationType type, BsonObjectId id)
    {
        switch (type)
        {
            case ENotificationType.SMS:
                return new NotificationSMSDeliveryFailureEvent(id);
            case ENotificationType.Email:
                return new NotificationEmailDeliveryFailureEvent(id);
            case ENotificationType.PushNotification:
                return new NotificationPushDeliveryFailureEvent(id);
            case ENotificationType.Letter:
                return new NotificationLetterDeliveryFailureEvent(id);
            case ENotificationType.WhatsApp:
                return new NotificationWhatsAppDeliveryFailureEvent(id);
            
            default:
                throw new ArgumentException($"Type {type.ToString()} not found");;
        }
    }
}