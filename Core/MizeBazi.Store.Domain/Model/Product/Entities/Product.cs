using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Domain;

public class Product : AggregateRoot
{
    public ProductBasicInfo BasicInfo { get; private set; }

    public ProductPricing Pricing { get; private set; }

    public Sku SKU { get; private set; }

    public ProductImages Imgs { get; private set; }

    public ProductCategory Categories { get; private set; }

    public Product(long id, Guid unicId, DateTime date,
        ProductBasicInfo basicInfo,
        ProductPricing pricing,
        Sku sku,
        ProductImages images,
        ProductCategory categoryIds
        ) : base(id, unicId, date)
    {
        BasicInfo = basicInfo;
        Pricing = pricing;
        SKU = sku;
        Imgs = images;
        Categories = categoryIds;
    }
}

