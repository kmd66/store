using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public record GetBrandQuery : DbGetRecord, IQuery<Result<GetBrandResult>>;
public record ListBrandQuery : PaginationRecord, IQuery<Result<IEnumerable<ListBrandResult>>>
{
    public string Name { get; init; }
}



public record GetBrandResult : BaseBrandRecordModel;
public record ListBrandResult : BaseBrandRecordModel
{
    public int TotalCount { get; init; }
}