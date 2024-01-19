using System.Text.Json.Serialization;
using MediatR;

namespace Notification.Core.Common.CQRS;

public abstract class Event : Message, INotification
{ 
    protected Event(string exchange, string routerKey)
    {
        Exchange = exchange;
        RouterKey = routerKey;
    }

    public Event()
    {
        
    }
    
    
    public string Exchange { get; private set; }
    public string RouterKey { get; private set; }
}