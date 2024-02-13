using System.Runtime.Serialization;
using Notification.Core.Common.CQRS;

namespace Notification.API.Application.Commands.Views;

[DataContract]
public class CreateNotificationCommandView : View
{
    protected CreateNotificationCommandView()
    {
        
    }
    
    public CreateNotificationCommandView(CreateNotificationCommand command)
    {
        CorrelationId = command.AggregateId;
        Runtime = DateTime.UtcNow - command.CreatedAt;
    }
    
    [DataMember]
    public Guid CorrelationId { get; private set; }
    
    [DataMember]
    public TimeSpan Runtime { get; private set; }
}