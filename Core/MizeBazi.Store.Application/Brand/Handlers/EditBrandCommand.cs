using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public class EditBrandCommandHandler(
    IBrandWrite dataSource
    ) : CommandBase<EditeBrandCommand, Result>
{

    public override Task<Result> Handle(EditeBrandCommand command)
        => dataSource.EditeAsync(command);
}