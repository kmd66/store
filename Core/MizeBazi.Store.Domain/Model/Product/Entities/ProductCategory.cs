using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Domain;

public sealed class ProductCategory //: EventForEntity
{
    public long ProductId { get; private set; }
    private List<long> _categoryIds { get; set; }
    public IReadOnlyList<long> CategoryIds => _categoryIds.AsReadOnly();

    private const int MaxItem = 5;

    public ProductCategory(long productId, List<long> categoryIds)
    {
        if (productId == 0)
            throw new ValidatorException(ProductConstants.Error_CategoryProductId);
        if (categoryIds.Count < 1)
            throw new ValidatorException(ProductConstants.Error_CategoryId);
        ProductId = productId;

        if (categoryIds.Count > 5)
            _categoryIds = categoryIds.Take(5).ToList();
        else
            _categoryIds = categoryIds;
    }

    public bool CategoryIdsEqual(IEnumerable<long> otherCategoryIds)
    {
        if (otherCategoryIds == null)
            return false;

        return CategoryIds.Count == otherCategoryIds.Count() &&
               CategoryIds.All(otherCategoryIds.Contains) &&
               otherCategoryIds.All(CategoryIds.Contains);
    }

    public override bool Equals(object obj)
    {
        if (obj is not ProductCategory other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        return ProductId == other.ProductId &&
               CategoryIds.SequenceEqual(other.CategoryIds);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17 * 23 + ProductId.GetHashCode();

            foreach (var categoryId in CategoryIds.OrderBy(x => x))
            {
                hash = hash * 23 + categoryId.GetHashCode();
            }

            return hash;
        }
    }
}
