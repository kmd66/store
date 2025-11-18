using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application.Interfaces;

public interface IProductWrite
{
    Task<Result<long>> AddAsync(Product model, CancellationToken cancellationToken = default);
    Task<Result> EditeAsync(Product model, CancellationToken cancellationToken = default);
    Task<Result> EditePublishAsync(PublishProductCommand model, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(DeleteProductCommand model, CancellationToken cancellationToken = default);

}