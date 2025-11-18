using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application.Interfaces;

public interface IProductWrite
{
    Task<Result<long>> AddAsync(Product model, CancellationToken ct = default);
    Task<Result> EditeAsync(Product model, CancellationToken ct = default);
    Task<Result> EditePublishAsync(PublishProductCommand model, CancellationToken ct = default);
    Task<Result> DeleteAsync(DeleteProductCommand model, CancellationToken ct = default);

}