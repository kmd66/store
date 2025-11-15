using MizeBazi.Store.Common.Shared;
using System.Reflection;

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
        try
        {
            var handlerInfo = GetHandler<ICommand<TResponse>, TResponse>(command);
            var modelType = handlerInfo.ModelType;
            var handler = handlerInfo.Handler;

            await CallBehavior(modelType, command, cancellationToken);
            return await CallHandle<TResponse>(command, handler, cancellationToken);
        }
        catch (Exception e)
        {
            throw new Exception();
        }
    }

    public async Task<TResponse> Send<TResponse>(
        IQuery<TResponse> query,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var handlerInfo = GetHandler<IQuery<TResponse>, TResponse>(query);
            var modelType = handlerInfo.ModelType;
            var handler = handlerInfo.Handler;

            await CallBehavior(modelType, query, cancellationToken);
            return await CallHandle<TResponse>(query, handler, cancellationToken);
        }
        catch (Exception e)
        {
            throw new Exception();
        }
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
    public async Task<TAfterBehavior> Pipeline<TAfterBehavior>(IRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var reqInterface = request.GetType().GetInterfaces().First(i =>i.IsGenericType && i.GetGenericTypeDefinition().Name == "IRequest`1");

            var requestInterface = request.GetType().GetInterfaces()
            .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>))
            .GetGenericArguments()[0];

            var responseType1 = requestInterface.GetGenericArguments()[0];
            var responseType = reqInterface.GetGenericArguments()[0];

            var t = typeof(TAfterBehavior);
            var afterHandlerType = typeof(IPipelineBehavior<,>).MakeGenericType(requestInterface, typeof(TAfterBehavior));
            var afterHandler = _provider.GetService(afterHandlerType);

            if (afterHandler == null)
            {
                throw new InvalidOperationException($"No handler registered2 for {afterHandlerType.FullName}");
            }

            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), requestInterface);
            var handler = _provider.GetService(handlerType);
            if (handler == null)
            {
                throw new InvalidOperationException($"No handler registered for {handlerType.FullName}");
            }

            await CallBehavior(responseType, request, cancellationToken);

            var response = await CallHandleDynamic(
                responseType,
                request,
                handler,
                cancellationToken
            );

            dynamic dynHandler = afterHandler;
            var handleMethod = dynHandler.GetType().GetMethod("Handle", new[] { response.GetType() });

            var task = (Task)handleMethod.Invoke(dynHandler, new object[] { response });
            await task.ConfigureAwait(false);

            var result = task.GetType().GetProperty("Result").GetValue(task);
            return (TAfterBehavior)result;
        }
        catch (Exception e)
        {
            throw new Exception();
        }

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
    private async Task<object> CallHandleDynamic(
        Type responseType,
        object request,
        object handler,
        CancellationToken cancellationToken = default
    )
    {
        var method = typeof(MediatorAdapter)
            .GetMethod("CallHandle", BindingFlags.NonPublic | BindingFlags.Instance);

        var generic = method.MakeGenericMethod(responseType);
        var task = (Task)generic.Invoke(this, new object[] { request, handler, cancellationToken });
        await task.ConfigureAwait(false);
        return task.GetType().GetProperty("Result").GetValue(task);
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