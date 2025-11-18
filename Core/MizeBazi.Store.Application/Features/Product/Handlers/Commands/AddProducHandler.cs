using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application;

public class AddProducHandler(
    IProductWrite dataSource
    ) : CommandBase<AddProductCommand, Result<long>>
{

    public override async Task<Result<long>> Handle(AddProductCommand command, CancellationToken ct)
    {
        var categories = ProductFactory.Create(command.Id, command.categories);
        var product = ProductFactory.CreateForAdd(command, categories);
        return await dataSource.AddAsync(product, ct);
    }

}

