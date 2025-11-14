using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public class EditBanerCommandHandler(
    IBrandWrite dataSource
    ) : ICommandHandler<EditeBanerCommand, Result>
{

    public Task<Result> Handle(EditeBanerCommand command)
        => dataSource.EditeAsync(command);

    public Task<Result> Handle(EditeBanerCommand command, CancellationToken cancellationToken)
        => throw new NotImplementedException();
}