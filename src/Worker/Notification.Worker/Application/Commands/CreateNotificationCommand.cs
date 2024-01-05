using System.Runtime.Serialization;
using Notification.Core.Common.CQRS;
using Notification.Core.Domain.Enums;

namespace Notification.Worker.Application.Commands;


[DataContract]
public class CreateNotificationCommand : Command
{ 
    [DataMember]
    public string Recipient { get; set; }
    
    [DataMember] 
    public ENotificationType Type { get; set; }
    
    [DataMember]
    public int MessageLayout { get; set; }
    
    [DataMember]
    public List<CreateNotificationParamsCommand> Params { get; set; }
}


[DataContract]
public class CreateNotificationParamsCommand
{ 
    [DataMember]
    public string Key { get; set; }
    
    [DataMember]
    public string Value { get; set; }
}