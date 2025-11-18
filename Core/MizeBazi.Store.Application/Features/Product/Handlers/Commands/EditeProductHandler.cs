using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application;

public class EditeProductHandler(
    IProductWrite dataSource
    ) : CommandBase<EditeProductCommand, Result>
{
    public override async Task<Result> Handle(EditeProductCommand command, CancellationToken ct)
    {
        var product = ProductFactory.CreateForEdite(command);

        return await dataSource.EditeAsync(product, ct);
    }
}



