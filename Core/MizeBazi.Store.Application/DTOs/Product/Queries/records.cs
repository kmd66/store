using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public record GetProductByIdQuery : DbGetRecord, IQuery<GetProductResult>;
public record GetProductBySkuQuery(string sku) : IQuery<GetProductResult>;

public record ListProductQuery : PaginationRecord, IQuery<ListProductResult>
{
    public string Name { get; init; }

    public string Description { get; init; }

    public decimal? MaxPrice { get; init; }

    public decimal? MinPrice { get; init; }

    public long? BrandId { get; init; }

    public long? CategoryId { get; init; }

    public bool? IsPublished { get; init; }

    public bool? HasDiscount { get; init; }

    public bool? HasQuantity { get; init; }
}

public record GetProductResult : ResultRecord
{
    public BaseProductResult data { get; init; }
}
public record ListProductResult : ResultRecord
{
    public IEnumerable<BaseProductResult> data { get; init; }
}