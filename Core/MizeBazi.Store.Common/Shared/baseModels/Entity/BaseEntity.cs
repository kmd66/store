using MizeBazi.Store.Common.Abstractions;

namespace MizeBazi.Store.Common.Shared;

public abstract class EventForEntity
{
    protected readonly List<DomainEvent> _listEvents = new();

    protected bool IsConfirmed = false;

    public List<DomainEvent> DomainEvents() => _listEvents;

    public virtual void Confirm(EventType t)
    {
        IsConfirmed = true;
    }

    public void ClearDomainEvents() => _listEvents.Clear();
}
public abstract class Entity: EventForEntity
{
    public Guid UnicId { get; private set; }
    public DateTime Date { get; private set; } = DateTime.UtcNow;

    protected Entity() { }

    protected Entity(Guid unicId, DateTime date)
    {
        UnicId = unicId;
        Date = date;
    }

}

public abstract class AggregateRoot : Entity
{
    public long Id { get; private set; }

    protected AggregateRoot(long id, Guid unicId, DateTime date) : base(unicId, date)
    {
        {
            Id = id;
        }
    }
}