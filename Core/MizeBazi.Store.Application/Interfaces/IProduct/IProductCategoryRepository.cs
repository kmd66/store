using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application.Interfaces;

public interface IProductCategoryRepository
{
    Task<Result<IEnumerable<DbProductCategory>>> ListByIdAsync(long productId, CancellationToken ct = default);

    Task<Result<IEnumerable<long>>> GetCategoryIdsAsync(ProductCategory model, CancellationToken ct = default);

    Task<Result> EditeAsync(ProductCategory model, CancellationToken ct = default);
}