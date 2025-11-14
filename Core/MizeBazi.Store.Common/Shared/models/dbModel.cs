namespace MizeBazi.Store.Common.Shared;
public abstract record DbGetRecord
{
    public long? Id { get; set; }
    public Guid? UnicId
    {
        get; set;
    }
}
public class DbGet
{
    public long? Id { get; set; }
    public Guid? UnicId { get; set; } 
}

public abstract record PaginationRecord
{
    private int _pageIndex = 1;
    private int _pageSize = 10;
    public bool? IsDeleted { get; init; }

    public int PageIndex {
        get => _pageIndex;
        init => _pageIndex = value switch
        {
            < 1 => 1,
            > 1000 => 1000,
            _ => value
        };
    }
    public int PageSize
    {
        get => _pageSize;
        init => _pageSize = value switch
        {
            < 1 => 1,
            > 100 => 100,
            _ => value
        };
    }
}
public class Pagination
{
    public bool IsDeleted { get; set; } = false;
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

