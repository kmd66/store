using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application.Interfaces;

public interface ICategoryRead
{
    Task<Result<GetCategoryResult>> GetAsync(GetCategoryQuery model, CancellationToken cancellationToken = default);

    Task<Result<IEnumerable<ListCategoryResult>>> ListAsync(ListCategoryQuery model, CancellationToken cancellationToken = default);

}