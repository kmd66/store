using MizeBazi.Store.Common.Abstractions;
using System.ComponentModel;

namespace MizeBazi.Store.Common.Shared;
public abstract record DbGetRecord
{
    public long? Id { get; set; }
    public Guid? UnicId
    {
        get; set;
    }
    public void Check<T>(IAppLogger<T> logger, string errorMsg = "id null")
    {
        if (Check())
        {
            logger.LogWarning($"Exception message: {errorMsg}");
            throw new ValidatorException($"Brand Get Exception: {errorMsg}");
        }
    }
    public void Check(string errorMsg)
    {

        if (Check())
        {
            throw new ValidatorException($"Brand Get Exception: {errorMsg}");
        }
    }
    public bool Check() => Id == null && UnicId == null;
}
public abstract class DbGet
{
    public long? Id { get; set; }
    public Guid? UnicId { get; set; }

}

public abstract record PaginationRecord
{
    [DefaultValue(1)]
    private int _pageIndex { get; init; } = 1;
    [DefaultValue(10)]
    private int _pageSize { get; init; } = 10;

    [DefaultValue(false)]
    public bool? IsDeleted { get; init; } = false;

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
public abstract class Pagination
{
    [DefaultValue(false)]
    public bool IsDeleted { get; set; } = false;
    [DefaultValue(1)]
    public int PageIndex { get; set; } = 1;
    [DefaultValue(10)]
    public int PageSize { get; set; } = 10;
}

