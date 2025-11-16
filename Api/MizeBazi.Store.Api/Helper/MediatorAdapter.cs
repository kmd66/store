using MizeBazi.Store.Common.Abstractions;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MizeBazi.Store.Api.Helper;

public class MediatorAdapter(IServiceProvider provider) : MediatorAdapterBase(provider: provider), IAppMediator
{


    public async Task<TResponse> Send<TResponse>(
        ICommand<TResponse> command,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var handlerInfo = GetHandler<ICommand<TResponse>, TResponse>(command);
            await CallBehavior(handlerInfo.ModelType, command, cancellationToken);
            return await CallHandle<TResponse>(command, handlerInfo.Handler, cancellationToken);
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
            await CallBehavior(handlerInfo.ModelType, query, cancellationToken);
            return await CallHandle<TResponse>(query, handlerInfo.Handler, cancellationToken);
        }
        catch (Exception e)
        {
            throw new Exception();
        }
    }

    public async Task<TAfterBehavior> Pipeline<TAfterBehavior>(IRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var handlerInfo = GetPiplineHandler<TAfterBehavior>(request);

            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), handlerInfo.RequestInterface);
            var handler = provider.GetService(handlerType);
            if (handler == null)
            {
                throw new InvalidOperationException($"No handler registered for {handlerType.FullName}");
            }

            await CallBehavior(handlerInfo.ResponseType, request, cancellationToken);

            var response = await CallHandleDynamic(
                handlerInfo.ResponseType,
                request,
                handler,
                cancellationToken
            );

            return await CallPipeline<TAfterBehavior>(handlerInfo.Handler, response);
        }
        catch (Exception e)
        {
            throw new Exception();
        }

    }


    private async Task CallBehavior(Type t, dynamic obj, CancellationToken cancellationToken = default)
    {
        if (t == null) return;

        var type = typeof(IBehaviorHandler<>).MakeGenericType(t);
        var handlers = provider.GetServices(type).ToList();
        await CallBehaviorBase(handlers, obj, cancellationToken);
    }


    public async Task<TAfterBehavior> Pipline<TResponse, TAfterBehavior>(
        IRequest<TResponse> request,
        IRequest<TAfterBehavior> afterBehavior = null,
        CancellationToken cancellationToken = default
    )
    {
        var afterType = afterBehavior.GetType();
        var afterHandlerType = typeof(IPipelineBehavior<,>).MakeGenericType(typeof(TResponse), typeof(TAfterBehavior));

        var afterHandler = provider.GetService(afterHandlerType);
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
}