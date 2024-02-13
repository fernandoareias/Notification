using System.Runtime.Serialization;
using Notification.Core.Common.CQRS;

namespace Notification.Worker.Application.Commands.Send.Base;

public abstract class SendNotificationCommand  : Command
{
    protected SendNotificationCommand()
    {
        
    }
    public SendNotificationCommand(CreateNotificationCommand request)
    {
        Recipient = request.Recipient;
        MessageLayout = request.MessageLayout; 
        Params = request.Params.Select(c => new SendNotificationParamsCommand(c)).ToList();
    }       
    
    [DataMember]
    public string Recipient { get; set; }
     
    
    [DataMember]
    public int MessageLayout { get; set; }
    
    [DataMember]
    public List<SendNotificationParamsCommand> Params { get; set; }
}


[DataContract]
public class SendNotificationParamsCommand
{
    protected SendNotificationParamsCommand()
    {
        
    }
    public SendNotificationParamsCommand(CreateNotificationParamsCommand request)
    {
        Key = request.Key;
        Value = request.Value;
    }
    [DataMember]
    public string Key { get; private set; }
    
    [DataMember]
    public string Value { get; private set; }
}