using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Domain;

public static class ProductFactory
{
    public static Product Create(
        ProductBasicInfo basicInfo,
        ProductPricing pricing,
        Sku sku,
        ProductImages images,
        ProductCategory categories) => Create(0, Guid.NewGuid(), DateTime.UtcNow, basicInfo, pricing, sku, images, categories);
    public static Product CreateForAdd(dynamic model, ProductCategory categories)
    {
        var basicInfo = BasicInfo(model);
        var pricing = Pricing(model);
        var images = ProductImages.From(model.Images);
        var sku = Sku.From(model.SKU);

        return Create(0, Guid.NewGuid(), DateTime.UtcNow, basicInfo, pricing, sku, images, categories);
    }
    public static Product CreateForEdite(dynamic model) 
    {
        var categories = Create(1, [1]);
        return CreateForAdd(model, categories);
    }

    public static Product Create(long id, Guid unicId, DateTime date,
        ProductBasicInfo basicInfo,
        ProductPricing pricing,
        Sku sku,
        ProductImages images,
        ProductCategory categories)
    {
        if (categories == null || categories.CategoryIds.Count < 1)
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

    public static ProductCategory Create(long productId, List<long> categoryIds)
        => new ProductCategory(productId, categoryIds);


    private static ProductBasicInfo BasicInfo(dynamic model)
    {
        return ProductBasicInfo.From(
            model.Name,
            model.Description,
            model.BrandId,
            model.Quantity,
            model.IsPublished
        );
    }
    private static ProductPricing Pricing(dynamic model)
    {
        return ProductPricing.From(
            model.Price,
            model.CompareAtPrice
        );
    }

}
