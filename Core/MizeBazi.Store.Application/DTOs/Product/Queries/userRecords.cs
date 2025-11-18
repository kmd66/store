using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public record ListProductUserQuery : PaginationRecord, IQuery<ListProductUserResult>
{
    public string Name { get; init; }

    public decimal? MaxPrice { get; init; }

    public decimal? MinPrice { get; init; }

    public long? BrandId { get; init; }

    public long? CategoryId { get; init; }

    //public bool? HasDiscount { get; init; }
}

public record GetProductUserResult : ResultRecord
{
    public BaseUserProductResult data { get; init; }
}
public record ListProductUserResult : ResultRecord
{
    public IEnumerable<BaseUserProductResult> data { get; init; }
}