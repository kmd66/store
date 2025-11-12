namespace MizeBazi.Store.Common.Abstractions;
public interface ICommand<TResponse> { }

public interface IQuery<TResponse> { }
public interface IBehaviorHandler<T>
{
    Task Handle(CancellationToken cancellationToken);
}

public interface ICommandHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);
}

public interface IQueryHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken);
}

public interface IAppMediator
{
    Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default);
    Task<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default);
}

public class MediatorAdapter : IAppMediator
{
    private readonly IServiceProvider _provider;

    public MediatorAdapter(IServiceProvider provider)
    {
        _provider = provider;
    }


    public async Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
    {
        if (command == null) throw new ArgumentNullException(nameof(command));

        var commandType = command.GetType();
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(commandType, typeof(TResponse));

        var handler = _provider.GetService(handlerType);
        if (handler == null)
        {
            throw new InvalidOperationException($"No handler registered for {handlerType.FullName}");
        }
        await behavior(commandType, cancellationToken);
        dynamic dynHandler = handler;
        var resultTask = (Task<TResponse>)dynHandler.Handle((dynamic)command, cancellationToken);
        return await resultTask.ConfigureAwait(false);
    }

    public async Task<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default)
    {
        if (query == null) throw new ArgumentNullException(nameof(query));

        var queryType = query.GetType();
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, typeof(TResponse));

        var handler = _provider.GetService(handlerType);
        if (handler == null)
        {
            throw new InvalidOperationException($"No handler registered for {handlerType.FullName}");
        }
        await behavior(queryType, cancellationToken);
        dynamic dynHandler = handler;
        var resultTask = (Task<TResponse>)dynHandler.Handle((dynamic)query, cancellationToken);
        return await resultTask.ConfigureAwait(false);
    }
    public async Task<TResponse> Sensd<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
    {
        if (command == null) throw new ArgumentNullException(nameof(command));

        var commandType = command.GetType();
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(commandType, typeof(TResponse));

        var handler = _provider.GetService(handlerType);
        if (handler == null)
        {
            throw new InvalidOperationException($"No handler registered for {handlerType.FullName}");
        }
        dynamic dynHandler = handler;
        var resultTask = (Task<TResponse>)dynHandler.Handle((dynamic)commandType, cancellationToken);
        return await resultTask.ConfigureAwait(false);
    }

    public async Task<TResponse> Sensd<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default)
    {
        if (query == null) throw new ArgumentNullException(nameof(query));

        var queryType = query.GetType();
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, typeof(TResponse));

        var handler = _provider.GetService(handlerType);
        if (handler == null)
        {
            throw new InvalidOperationException($"No handler registered for {handlerType.FullName}");
        }
        await behavior(queryType, cancellationToken);
        dynamic dynHandler = handler;
        var resultTask = (Task<TResponse>)dynHandler.Handle((dynamic)query, cancellationToken);
        return await resultTask.ConfigureAwait(false);
    }

    private async Task behavior(Type t, CancellationToken cancellationToken = default)
    {
        var type = typeof(IBehaviorHandler<>).MakeGenericType(t);
        var handler = _provider.GetService(type);
        if (handler != null)
        {
            dynamic dynHandler = handler;
            var resultTask = dynHandler.Handle(cancellationToken);
            await resultTask.ConfigureAwait(false);
        }
    }
}