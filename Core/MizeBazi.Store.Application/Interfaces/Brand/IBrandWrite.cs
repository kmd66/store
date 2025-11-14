using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application.Interfaces;

public interface IBrandWrite
{
    Task<Result> AddAsync(AddBanerCommand brand, CancellationToken cancellationToken = default);

    Task<Result> EditeAsync(EditeBanerCommand brand, CancellationToken cancellationToken = default);
}
