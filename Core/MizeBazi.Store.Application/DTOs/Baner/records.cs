using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public abstract record BaseBanerRecordModel : SoftDeleteEntityRecord
{
    public string Name { get; init; }

    public string Description { get; init; }

    public string LogoUrl { get; init; }
}


public record AddBanerCommand : BaseBanerRecordModel, ICommand<Result>;
public record EditeBanerCommand : BaseBanerRecordModel, ICommand<Result>;


public record GetBanerQuery : DbGetRecord, IQuery<Result<BaseBanerRecordModel>>;
public record ListBanerQuery(string Name) : PaginationRecord, IQuery<Result<IEnumerable<ListBanerResult>>>;


public record ListBanerResult(int TotalCount) : BaseBanerRecordModel;