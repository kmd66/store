namespace MizeBazi.Store.Common.Shared;
public abstract class BaseEntity
{
    public int Id { get; set; }
    public Guid UnicId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public abstract class SoftDeleteEntity : BaseEntity
{
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
}
