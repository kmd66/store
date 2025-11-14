using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application.Interfaces;

public interface IBrandRead
{
    Task<Result<BaseBanerRecordModel>> GetAsync(GetBanerQuery model, CancellationToken cancellationToken = default);

    Task<Result<IEnumerable<ListBanerResult>>> ListAsync(ListBanerQuery model, CancellationToken cancellationToken = default);

}