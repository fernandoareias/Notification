using MongoDB.Bson;
using Notification.Core.Domain.Enums;
using Notification.Worker.Domain.Events.Common;

namespace Notification.Worker.Domain.Events.Factories;

public static class NotificationDeliveryFailureEventFactory
{
    public static NotificationDeliveryFailureEvent Create(ENotificationType type, string CorrelationId)
    {
        switch (type)
        {
            case ENotificationType.SMS:
                return new NotificationSMSDeliveryFailureEvent(CorrelationId);
            case ENotificationType.Email:
                return new NotificationEmailDeliveryFailureEvent(CorrelationId);
            case ENotificationType.Push:
                return new NotificationPushDeliveryFailureEvent(CorrelationId);
            case ENotificationType.Letter:
                return new NotificationLetterDeliveryFailureEvent(CorrelationId);
            case ENotificationType.WhatsApp:
                return new NotificationWhatsAppDeliveryFailureEvent(CorrelationId);
            
            default:
                throw new ArgumentException($"Type {type.ToString()} not found");;
        }
    }
}