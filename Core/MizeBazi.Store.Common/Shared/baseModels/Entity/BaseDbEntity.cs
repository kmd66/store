namespace MizeBazi.Store.Common.Shared;
public abstract class DbEntity
{
    public long Id { get; set; }
    public Guid UnicId { get; set; }

}

public abstract class BaseDbEntity : DbEntity
{
    public DateTime Date { get; set; } = DateTime.UtcNow;
}

public abstract class SoftDeleteEntity : BaseDbEntity
{
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedDate { get; set; }
}