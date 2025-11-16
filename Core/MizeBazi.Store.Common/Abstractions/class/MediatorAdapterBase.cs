using System.Reflection;

namespace MizeBazi.Store.Common.Abstractions;
public record GetHandlerModel(Type ModelType, object Handler);
public record GetPiplineHandlerModel(Type ReqInterface, Type RequestInterface, Type ResponseType, Type ResponseType1, object Handler);
public abstract class MediatorAdapterBase
{
    protected readonly IServiceProvider provider;
    public MediatorAdapterBase(IServiceProvider p) 
    {
        provider = p;
    }

    protected GetPiplineHandlerModel GetPiplineHandler<TAfterBehavior>(IRequest request)
    {
        var reqInterface = request.GetType().GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition().Name == "IRequest`1");

        var requestInterface = request.GetType().GetInterfaces()
        .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>))
        .GetGenericArguments()[0];

        var responseType1 = requestInterface.GetGenericArguments()[0];
        var responseType = reqInterface.GetGenericArguments()[0];

        var afterHandlerType = typeof(IPipelineBehavior<,>).MakeGenericType(requestInterface, typeof(TAfterBehavior));
        var afterHandler = provider.GetService(afterHandlerType);

        if (afterHandler == null)
        {
            throw new InvalidOperationException($"No handler registered2 for {afterHandlerType.FullName}");
        }
        return new GetPiplineHandlerModel(reqInterface, requestInterface, responseType, responseType1, afterHandler);
    }

    protected async Task<TResponse> CallHandle<TResponse>(dynamic obj, dynamic dynHandler, CancellationToken cancellationToken = default)
    {

        if (cancellationToken == CancellationToken.None)
        {
            var resultTask1 = (Task<TResponse>)dynHandler.Handle(obj);
            return await resultTask1.ConfigureAwait(false);
        }

        var resultTask = (Task<TResponse>)dynHandler.Handle(obj, cancellationToken);
        return await resultTask.ConfigureAwait(false);

    }
    protected GetHandlerModel GetHandler<TRequest, TResponse>(TRequest request)
        where TRequest : IRequest<TResponse>
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var modelType = request.GetType();
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(modelType, typeof(TResponse));

        var handler = provider.GetService(handlerType);
        if (handler == null)
        {
            throw new InvalidOperationException($"No handler registered for {handlerType.FullName}");
        }

        return new GetHandlerModel(modelType, handler);
    }

    protected async Task CallBehaviorBase(List<object> handlers, dynamic obj, CancellationToken cancellationToken = default)
    {
        foreach (var handler in handlers)
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

    protected async Task<object> CallHandleDynamic(
        Type responseType,
        object request,
        object handler,
        CancellationToken cancellationToken = default
    )
    {
        var method = typeof(MediatorAdapterBase)
            .GetMethod("CallHandle", BindingFlags.NonPublic | BindingFlags.Instance);

        var generic = method.MakeGenericMethod(responseType);
        var task = (Task)generic.Invoke(this, new object[] { request, handler, cancellationToken });
        await task.ConfigureAwait(false);
        return task.GetType().GetProperty("Result").GetValue(task);
    }

    protected async Task<TAfterBehavior> CallPipeline<TAfterBehavior>(
        dynamic afterHandler, 
        object response,
        CancellationToken cancellationToken = default
    )
    {

        dynamic dynHandler = afterHandler;
        var handleMethod = dynHandler.GetType().GetMethod("Handle", new[] { response.GetType() });

        var task = (Task)handleMethod.Invoke(dynHandler, new object[] { response });
        await task.ConfigureAwait(false);

        var result = task.GetType().GetProperty("Result").GetValue(task);
        return (TAfterBehavior)result;
    }
}