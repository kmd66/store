using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application.Interfaces;

public interface ICategoryWrite
{
    Task<Result> AddAsync(AddCategoryCommand Category, CancellationToken cancellationToken = default);

    Task<Result> EditeAsync(EditeCategoryCommand Category, CancellationToken cancellationToken = default);
}
