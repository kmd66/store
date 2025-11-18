using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public record AddProductCommand : BaseProductRecord, ICommand<Result<long>>
{
    public List<long> Categories { get; init; }
}
public record AddProductCommanda : BaseProductRecord, ICommand<Result<long>>{
}

public record EditeProductCommand : BaseProductRecord, ICommand<Result>;

public record EditeProductCategoryCommand(long Id, List<long> Categories) : ICommand<Result>;

public record DeleteProductCommand(long Id, bool State) : ICommand<Result>;

public record PublishProductCommand(long Id, bool State) : ICommand<Result>;

