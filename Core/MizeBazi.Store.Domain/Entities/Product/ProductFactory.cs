using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Domain;

public static class ProductFactory
{
    public static Product Create(
        ProductBasicInfo basicInfo,
        ProductPricing pricing,
        Sku sku,
        ProductImages images,
        List<ProductCategory> categories) => Create(0, Guid.NewGuid(), DateTime.UtcNow, basicInfo, pricing, sku, images, categories);

    public static Product Create(long id, Guid unicId, DateTime date,
        ProductBasicInfo basicInfo,
        ProductPricing pricing,
        Sku sku,
        ProductImages images,
        List<ProductCategory> categories)
    {
        if (categories == null || categories.Count < 1)
            throw new ValidatorException(ProductConstants.Error_Category);

        var product = new Product(
            id: id,
            unicId,
            date,
            basicInfo,
            pricing,
            sku,
            images,
            categories
        );

        return product;
    }
}
