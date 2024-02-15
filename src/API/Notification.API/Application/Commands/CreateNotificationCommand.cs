using System.Runtime.Serialization;
using Notification.Core.Domain.Enums;
using Notification.API.DTOs.Requests;
using Notification.Core.Common.CQRS;
using Notification.API.Application.Commands.Validations;

namespace Notification.API.Application.Commands;

[DataContract]
public class CreateNotificationCommand : Command
{
    protected CreateNotificationCommand()
    {
        
    }
    public CreateNotificationCommand(CreateNotificationRequest request)
    {
        Recipient = request.Recipient;
        MessageLayout = request.MessageLayout;
        Type = request.Type;
        Params = request.Params.Select(c => new CreateNotificationParamsCommand(c)).ToList();
    }
    
    [DataMember]
    public string Recipient { get; set; }
    
    [DataMember] 
    public ENotificationType Type { get; set; }
    
    [DataMember]
    public int MessageLayout { get; set; }
    
    [DataMember]
    public List<CreateNotificationParamsCommand> Params { get; set; }


    public override bool IsValid()
    {
        return new CreateNotificationValidations().Validate(this).Errors.Any() is false;
    }
}


[DataContract]
public class CreateNotificationParamsCommand
{
    protected CreateNotificationParamsCommand()
    {
        
    }
    public CreateNotificationParamsCommand(CreateNotificationParamsRequest request)
    {
        Key = request?.Key;
        Value = request?.Value;
    }
    [DataMember]
    public string? Key { get; private set; }
    
    [DataMember]
    public string? Value { get; private set; }
}