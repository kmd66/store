using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public class AddBrandCommandHandler(
    IBrandWrite dataSource
    ) : CommandBase<AddBrandCommand, Result>
{

    public override Task<Result> Handle(AddBrandCommand request)
        => dataSource.AddAsync(request);

}