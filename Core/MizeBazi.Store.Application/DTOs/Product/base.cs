using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;
using System.Text.Json;

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
    public string BrandName { get; init; }
}
public abstract record BaseProductRecord : BaseProductUserRecord
{
    public long Id { get; init; }
    public DateTime Date { get; init; }
    public bool IsDeleted { get; init; }
    public DateTime? DeletedDate { get; init; }

    public long BrandId { get; init; }
    public bool IsPublished { get; init; }
}
public record BaseUserProductResult: BaseProductUserRecord
{
    public int TotalCount { get; init; }
    public string CategoriesJson { get; init; }
    public List<CategoryForProdact> Categories => CategoriesJson.IsNullOrEmpty()
          ? new List<CategoryForProdact>()
          : JsonSerializer.Deserialize<List<CategoryForProdact>>(CategoriesJson);
}

public record BaseProductResult: BaseProductRecord
{
    public int TotalCount { get; init; }
    public string CategoriesJson { get; init; }
    public List<CategoryForProdact> Categories => CategoriesJson.IsNullOrEmpty()
          ? new List<CategoryForProdact>()
          : JsonSerializer.Deserialize<List<CategoryForProdact>>(CategoriesJson);
}

public record CategoryForProdact
{
    public long Id { get; init; }
    public string Name { get; init; }
}