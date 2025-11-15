using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public class EditBanerCommandHandler(
    IBrandWrite dataSource
    ) : CommandBase<EditeBanerCommand, Result>
{

    public override Task<Result> Handle(EditeBanerCommand command)
        => dataSource.EditeAsync(command);
}