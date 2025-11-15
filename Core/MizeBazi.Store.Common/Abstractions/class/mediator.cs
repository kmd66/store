namespace MizeBazi.Store.Common.Abstractions;

public class MediatorAdapter : IAppMediator
{
    private readonly IServiceProvider _provider;

    public MediatorAdapter(IServiceProvider provider)
    {
        _provider = provider;
    }


    public async Task<TResponse> Send<TResponse>(
        ICommand<TResponse> command,
        CancellationToken cancellationToken = default
    )
    {
        var handlerInfo = GetHandler<ICommand<TResponse>, TResponse>(command);
        var modelType = handlerInfo.ModelType;
        var handler = handlerInfo.Handler;

        await CallBehavior(modelType, command, cancellationToken);
        return await CallHandle<TResponse>(command, handler, cancellationToken);
    }

    public async Task<TResponse> Send<TResponse>(
        IQuery<TResponse> query,
        CancellationToken cancellationToken = default
    )
    {
        var handlerInfo = GetHandler<IQuery<TResponse>, TResponse>(query);
        var modelType = handlerInfo.ModelType;
        var handler = handlerInfo.Handler;

        await CallBehavior(modelType, query, cancellationToken);
        return await CallHandle<TResponse>(query, handler, cancellationToken);

    }


    public async Task<TAfterBehavior> Pipline<TResponse, TAfterBehavior>(
        IRequest<TResponse> request,
        IRequest<TAfterBehavior> afterBehavior = null,
        CancellationToken cancellationToken = default
    )
    {

        
        var afterType = afterBehavior.GetType();
        var afterHandlerType = typeof(IPipelineBehavior<,>).MakeGenericType(typeof(TResponse), typeof(TAfterBehavior));


        var afterHandler = _provider.GetService(afterHandlerType);
        if (afterHandler == null)
        {
            throw new InvalidOperationException($"No handler registered for {afterHandlerType.FullName}");
        }

        var handlerInfo = GetHandler<IRequest<TResponse>, TResponse>(request);
        var modelType = handlerInfo.ModelType;
        var handler = handlerInfo.Handler;

        await CallBehavior(modelType, request, cancellationToken);

        var response = await CallHandle<TResponse>(request, handler, cancellationToken);


        dynamic dynHandler = afterHandler;
        if (cancellationToken == CancellationToken.None)
        {
            var resultTask1 = (Task<TAfterBehavior>)dynHandler.Handle(response);
            return await resultTask1.ConfigureAwait(false);
        }

        var resultTask = (Task<TAfterBehavior>)dynHandler.Handle(response, cancellationToken);
        return await resultTask.ConfigureAwait(false);

    }

    private (Type ModelType, object Handler) GetHandler<TRequest, TResponse>(TRequest request)
        where TRequest : IRequest<TResponse>
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var modelType = request.GetType();
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(modelType, typeof(TResponse));

        var handler = _provider.GetService(handlerType);
        if (handler == null)
        {
            throw new InvalidOperationException($"No handler registered for {handlerType.FullName}");
        }

        return (modelType, handler);
    }

    private async Task CallBehavior(Type t, dynamic obj, CancellationToken cancellationToken = default)
    {
        if (t == null) return;

        var type = typeof(IBehaviorHandler<>).MakeGenericType(t);
        var handler = _provider.GetService(type);
        if (handler != null)
        {
            dynamic dynHandler = handler;
            if (cancellationToken == CancellationToken.None)
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

    private async Task<TResponse> CallHandle<TResponse>(dynamic obj, dynamic dynHandler, CancellationToken cancellationToken = default)
    {

        if (cancellationToken == CancellationToken.None)
        {
            var resultTask1 = (Task<TResponse>)dynHandler.Handle(obj);
            return await resultTask1.ConfigureAwait(false);
        }

        var resultTask = (Task<TResponse>)dynHandler.Handle(obj, cancellationToken);
        return await resultTask.ConfigureAwait(false);

    }
    private async Task<TAfterBehavior> CallPipeline<TAfterBehavior>(IRequest<TAfterBehavior> t, dynamic obj, CancellationToken cancellationToken = default)
    {
        var modelType = t.GetType();
        var type = typeof(IPipelineBehavior<,>).MakeGenericType(modelType, typeof(TAfterBehavior));
        var handler = _provider.GetService(type);
        if (handler != null)
        {
            dynamic dynHandler = handler;
            if (cancellationToken == CancellationToken.None)
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

        return obj;
    }
}