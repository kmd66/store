using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public class AddBanerCommandHandler(
    IBrandWrite dataSource
    ) : CommandBase<AddBanerCommand, Result>
{

    public override Task<Result> Handle(AddBanerCommand request)
        => dataSource.AddAsync(request);

}