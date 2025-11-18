using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Domain;

public class ProductBasicInfo : ValueObject
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public long BrandId { get; private set; }
    public int Quantity { get; private set; }
    public bool IsPublished { get; private set; }

    public static ProductBasicInfo From(string name, string description, long brandId, int quantity, bool isPublished)
    {
        if (name.IsNullOrEmpty())
            throw new ValidatorException(ProductConstants.Error_Name);
        if (description.IsNullOrEmpty())
            throw new ValidatorException(ProductConstants.Error_Description);

        if (brandId == 0)
            throw new ValidatorException(ProductConstants.Error_BrandId);
        if (quantity < 0)
            throw new ValidatorException(ProductConstants.Error_Quantity);

        return new ProductBasicInfo
        {
            Name = name,
            BrandId = brandId,
            Description = description,
            Quantity = quantity,
            IsPublished = isPublished
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Description;
        yield return BrandId;
        yield return Quantity;
        yield return IsPublished;
    }
}

