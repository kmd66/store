using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Domain;

public class ProductPricing : ValueObject
{
    public decimal Price { get; private set; }

    public decimal CompareAtPrice { get; private set; }

    public decimal DiscountAmount => CompareAtPrice - Price;

    public decimal DiscountPercentage => CompareAtPrice > 0 ? (DiscountAmount / CompareAtPrice) * 100 : 0;
    
    public bool HasDiscount => Price < CompareAtPrice;


    private ProductPricing(decimal price, decimal compareAtPrice = 0)
    {
        if (Price < 1)
            throw new ValidatorException(ProductConstants.Error_Price);
        if (CompareAtPrice < 0)
            throw new ValidatorException(ProductConstants.Error_CompareAtPrice);

        Price = Math.Round(price);
        CompareAtPrice = Math.Round(compareAtPrice);
    }

    public static ProductPricing From(decimal price, decimal compareAtPrice = 0)
        => new ProductPricing(price, compareAtPrice);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Price;
        yield return CompareAtPrice;
    }

    public override string ToString() => Price.ToString();
}
