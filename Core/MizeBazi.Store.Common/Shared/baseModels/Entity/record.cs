namespace MizeBazi.Store.Common.Shared;

public abstract record EntityRecord();
public abstract record BaseEntityRecord
{
    public long Id { get; init; }
    public Guid UnicId { get; init; }
    public DateTime Date { get; init; }
}
public abstract record SoftDeleteEntityRecord: BaseEntityRecord
{
    public bool IsDeleted { get; init; }
    public DateTime? DeletedDate { get; init; }
}
