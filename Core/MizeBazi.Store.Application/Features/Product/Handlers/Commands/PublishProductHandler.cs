using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public class PublishProductHandler(
    IProductWrite dataSource
    ) : CommandBase<PublishProductCommand, Result>
{

    public override Task<Result> Handle(PublishProductCommand command)
        => dataSource.EditePublishAsync(command);
}