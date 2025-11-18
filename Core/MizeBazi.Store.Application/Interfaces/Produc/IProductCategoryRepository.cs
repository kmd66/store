using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application.Interfaces;

public interface IProductCategoryRepositorys
{
    Task<Result<IEnumerable<DbProductCategory>>> ListByIdAsync(long productId, CancellationToken cancellationToken = default);

    Task<Result> EditeAsync(ProductCategory model, CancellationToken cancellationToken = default);
}