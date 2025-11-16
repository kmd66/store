using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application.Interfaces;

public interface IBrandWrite
{
    Task<Result> AddAsync(AddBrandCommand brand, CancellationToken cancellationToken = default);

    Task<Result> EditeAsync(EditeBrandCommand brand, CancellationToken cancellationToken = default);
}
