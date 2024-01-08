namespace Notification.Core.Common.CQRS;

public abstract class AggregateRoot : Entity
{
    private List<Event> _events = new List<Event>();
    public IReadOnlyCollection<Event> Events => _events;

    public void AddEvent(Event @event)
    {
        _events.Add(@event);
    }
}