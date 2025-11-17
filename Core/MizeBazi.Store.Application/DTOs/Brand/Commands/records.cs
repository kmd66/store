using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public abstract record BaseBrandRecordModel : SoftDeleteEntityRecord
{
    public string Name { get; init; }

    public string Description { get; init; }

    public string LogoUrl { get; init; }
}


public record AddBrandCommand : BaseBrandRecordModel, ICommand<Result>;
public record EditeBrandCommand : BaseBrandRecordModel, ICommand<Result>;
