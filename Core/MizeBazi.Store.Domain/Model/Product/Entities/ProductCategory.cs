using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Domain;

public sealed class ProductCategory //: EventForEntity
{
    public long ProductId { get; private set; }
    public long CategoryId { get; private set; }

    public ProductCategory(long productId, long categoryId)
    {
        if (productId == 0)
            throw new ValidatorException(ProductConstants.Error_CategoryProductId);
        if (categoryId == 0)
            throw new ValidatorException(ProductConstants.Error_CategoryId);
        ProductId = productId;
        CategoryId = categoryId;
    }
}
