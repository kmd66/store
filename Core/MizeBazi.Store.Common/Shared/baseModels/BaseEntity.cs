namespace MizeBazi.Store.Common.Shared;
public abstract class BaseEntity
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
