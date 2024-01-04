using System.Runtime.Serialization;
using Notification.API.Domain.Enums;

namespace Notification.API.DTOs.Requests;

[DataContract]
public class CreateNotificationRequest
{
    [DataMember]
    public string Recipient { get; set; }
    
    [DataMember] 
    public NotificationType Type { get; set; }
    
    [DataMember]
    public int MessageLayout { get; set; }
    
    [DataMember]
    public List<CreateNotificationParamsRequest> Params { get; set; }
}

[DataContract]
public class CreateNotificationParamsRequest
{
    [DataMember]
    public string Key { get; set; }
    
    [DataMember]
    public string Value { get; set; }
}