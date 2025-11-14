namespace MizeBazi.Store.Common.Abstractions;
public interface ICommand<TResponse> { }

public interface IQuery<TResponse> { }
public interface IBehaviorHandler<T>
{
    Task Handle(T command, CancellationToken cancellationToken);
    Task Handle(T command);
}

public interface ICommandHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);
    Task<TResponse> Handle(TCommand command);
}

public interface IQueryHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken);
    Task<TResponse> Handle(TQuery query);
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
        var dynamicModel = (dynamic)command;
        await behavior(commandType, dynamicModel, cancellationToken);
        dynamic dynHandler = handler;
        return await callHandle<TResponse>(dynamicModel, handler, cancellationToken);
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

        var dynamicModel = (dynamic)query;
        await behavior(queryType, dynamicModel, cancellationToken);
        return await callHandle<TResponse>(dynamicModel, handler, cancellationToken);
    }
    private async Task behavior(Type t, dynamic obj, CancellationToken cancellationToken = default)
    {
        var type = typeof(IBehaviorHandler<>).MakeGenericType(t);
        var handler = _provider.GetService(type);
        if (handler != null)
        {
            dynamic dynHandler = handler;
            if(cancellationToken == CancellationToken.None)
            {
                var resultTask = dynHandler.Handle(obj);
                await resultTask.ConfigureAwait(false);
            }
            else
            {
                var resultTask = dynHandler.Handle(obj, cancellationToken);
                await resultTask.ConfigureAwait(false);
            }
        }
    }

    private async Task<TResponse> callHandle<TResponse>(dynamic obj, dynamic dynHandler, CancellationToken cancellationToken = default)
    {

        if (cancellationToken == CancellationToken.None)
        {
            var resultTask1 = (Task<TResponse>)dynHandler.Handle(obj);
            return await resultTask1.ConfigureAwait(false);
        }

        var resultTask = (Task<TResponse>)dynHandler.Handle(obj, cancellationToken);
        return await resultTask.ConfigureAwait(false);

    }
}