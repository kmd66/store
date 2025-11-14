using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public class AddBanerCommandHandler(
    IBrandWrite dataSource
    ) : ICommandHandler<AddBanerCommand, Result>
{

    public Task<Result> Handle(AddBanerCommand command)
        => dataSource.AddAsync(command);

    public Task<Result> Handle(AddBanerCommand command, CancellationToken cancellationToken) 
        => throw new NotImplementedException();
}