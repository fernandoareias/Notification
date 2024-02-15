using System.Runtime.Serialization;
using Notification.Core.Common.CQRS;

namespace Notification.API.Application.Commands.Views;

[DataContract]
public class CreateNotificationCommandView : View
{
    protected CreateNotificationCommandView()
    {
        
    }
     

    public CreateNotificationCommandView(Guid correlationId, DateTime createdAt)
    {

        if (correlationId == Guid.Empty)
            throw new ArgumentException(nameof(correlationId));

        if (createdAt == DateTime.MinValue || createdAt == DateTime.MaxValue)
            throw new ArgumentException(nameof(createdAt));

        CorrelationId = correlationId;
        Runtime = DateTime.UtcNow - createdAt;
    }

    [DataMember]
    public Guid CorrelationId { get; private set; }
    
    [DataMember]
    public TimeSpan Runtime { get; private set; }
}