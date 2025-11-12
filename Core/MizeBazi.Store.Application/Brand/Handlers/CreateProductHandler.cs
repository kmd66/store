using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application;

public record CreateProductCommand(string Name, decimal Price) : ICommand<Result<Guid>>;

public class CreateProductHandler(
    AutoEventDispatcher dispatcher,
    IBrandWrite brandWrite,
    IBrandRead brandRead
    ) : ICommandHandler<CreateProductCommand, Result<Guid>>
{
    private readonly AutoEventDispatcher _dispatcher = dispatcher;
    private readonly IBrandWrite _brandWrite = brandWrite;
    private readonly IBrandRead  _brandRead = brandRead;
    public async Task<Result<Guid>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var brand = Brand.CreateFromBaseModel(new DbBrand());
        await _dispatcher.ExecuteEvents(brand, EventType.Add);
        _=_brandWrite.AddAsync(new DbBrand());
        _=_brandRead.GetAsync(0);
        await Task.Delay(50, cancellationToken);
        return Result<Guid>.Successful(data: Guid.NewGuid());
    }
}
public class CreateProductValidator : IBehaviorHandler<CreateProductCommand>
{
    public Task Handle(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}