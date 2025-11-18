using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public abstract record BaseProductUserRecord
{
    public Guid UnicId { get; init; }

    public string Name { get; init; }

    public string Images { get; init; }

    public decimal Price { get; init; }

    public decimal CompareAtPrice { get; init; }

    public string Description { get; init; }

    public int Quantity { get; init; }

    public string SKU { get; init; }

    public long BrandId { get; init; }
}
public abstract record BaseProductRecord : BaseProductUserRecord
{
    public long Id { get; init; }
    public DateTime Date { get; init; }
    public bool IsDeleted { get; init; }
    public DateTime? DeletedDate { get; init; }

    public bool IsPublished { get; init; }
}
public record BaseUserProductResult: BaseProductUserRecord
{
    public long BrandName { get; init; }
    public long CategoryName { get; init; }
}

public record BaseProductResult: BaseProductRecord
{
    public long BrandName { get; init; }
    public long CategoryName { get; init; }
}


