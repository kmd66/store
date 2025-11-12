using MizeBazi.Store.Common.Abstractions;

namespace MizeBazi.Store.Common.Shared;

public abstract class  Entity
{
    protected readonly List<DomainEvent> _listEvents = new();
    protected bool IsConfirmed  = false;

    public List<DomainEvent> DomainEvents() => _listEvents;

    public virtual void Confirm()
    {
        IsConfirmed = true;
    }

    public void ClearDomainEvents() => _listEvents.Clear();

}

public abstract class BaseEntity: Entity
{
    public long Id { get; set; }
    public Guid UnicId { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}

public abstract class SoftDeleteEntity : BaseEntity
{
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedDate { get; set; }
}
