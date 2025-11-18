using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application.Interfaces;

public interface IProductRead
{
    Task<GetProductResult> GetAsync(
        long? id = null,
        Guid? unicId = null,
        string sku = null
        , CancellationToken ct = default
    );

    Task<ListProductResult> ListAsync(ListProductQuery model, CancellationToken ct = default);

    Task<ListProductUserResult> ListAsync(ListProductUserQuery model, CancellationToken ct = default);
}
