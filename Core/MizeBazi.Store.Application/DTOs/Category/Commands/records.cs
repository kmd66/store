using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public abstract record BaseCategoryRecordModel : SoftDeleteEntityRecord
{
    public string Name { get; init; }

    public string Description { get; init; }

    public string ImageUrl { get; init; }
}


public record AddCategoryCommand : BaseCategoryRecordModel, ICommand<Result>;
public record EditeCategoryCommand : BaseCategoryRecordModel, ICommand<Result>;