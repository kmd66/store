using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public record AddProductCommand : BaseProductRecord, ICommand<Result<long>>
{
    public List<long> categories { get; init; }
}
public record AddProductCommanda : BaseProductRecord, ICommand<Result<long>>{
}

public record EditeProductCommand : BaseProductRecord, ICommand<Result>;

public record EditeProductCategoryCommand(long id, List<long> categories) : ICommand<Result>;

public record DeleteProductCommand(long id) : ICommand<Result>;

public record PublishProductCommand(long id) : ICommand<Result>;

