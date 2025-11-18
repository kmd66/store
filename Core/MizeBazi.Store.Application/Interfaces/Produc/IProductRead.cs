using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application.Interfaces;

public interface IProductRead
{
    Task<GetProductResult> GetAsync(GetProductByIdQuery model, CancellationToken cancellationToken = default);

    Task<GetProductResult> GetBySkuAsync(string sku, CancellationToken cancellationToken = default);

    Task<ListProductResult> ListAsync(ListProductQuery model, CancellationToken cancellationToken = default);

    Task<ListProductUserResult> ListAsync(ListProductUserQuery model, CancellationToken cancellationToken = default);
}
