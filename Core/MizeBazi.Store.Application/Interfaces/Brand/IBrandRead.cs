using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application.Interfaces;

public interface IBrandRead
{
    Task<Result<GetBrandResult>> GetAsync(GetBrandQuery model, CancellationToken cancellationToken = default);

    Task<Result<IEnumerable<ListBrandResult>>> ListAsync(ListBrandQuery model, CancellationToken cancellationToken = default);

}