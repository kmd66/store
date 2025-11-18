using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public class DeleteProductHandler(
    IProductWrite dataSource
    ) : CommandBase<DeleteProductCommand, Result>
{

    public override Task<Result> Handle(DeleteProductCommand command)
        => dataSource.DeleteAsync(command);
}