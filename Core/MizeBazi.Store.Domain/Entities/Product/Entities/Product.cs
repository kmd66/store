using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Domain;

public class Product : AggregateRoot
{
    public ProductBasicInfo BasicInfo { get; private set; }

    public ProductPricing Pricing { get; private set; }

    public Sku SKU { get; private set; }

    public ProductImages Imgs { get; private set; }

    private List<ProductCategory> _categories = new();
    public IReadOnlyList<ProductCategory> Categories => _categories.AsReadOnly();
    
    public Product(long id, Guid unicId, DateTime date,
        ProductBasicInfo basicInfo,
        ProductPricing pricing,
        Sku sku,
        ProductImages images,
        List<ProductCategory> categoryIds
        ) : base(id, unicId, date)
    {
        BasicInfo = basicInfo;
        Pricing = pricing;
        SKU = sku;
        Imgs = images;
        _categories = categoryIds;
    }
}

