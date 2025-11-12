using MizeBazi.Store.Common.Abstractions;

namespace MizeBazi.Store.Common.Shared;

public class EventDispatcher(IServiceProvider serviceProvider) : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public Task DispatchLocalAsync(List<DomainEvent> domainEvent)
    {
        throw new NotImplementedException();
    }

    public async Task DispatchLocalAsync<TEvent>(TEvent domainEvent)
    {
        var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(typeof(TEvent));
        var handlers = (IEnumerable<object>)_serviceProvider.GetService(typeof(IEnumerable<>).MakeGenericType(handlerType));

        if (handlers == null) return;

        foreach (dynamic handler in handlers)
            await handler.Handle((dynamic)domainEvent);
    }

    public Task DispatchToBusAsync(DataSyncEvent model)
    {
        throw new NotImplementedException();
    }
}

public class AutoEventDispatcher
{
    private readonly IEventDispatcher _dispatcher;

    public AutoEventDispatcher(IEventDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public async Task ExecuteEvents<T>(Entity obj)
    {
        if(obj == null) return;
        obj.Confirm();
        await _dispatcher.DispatchLocalAsync(obj.DomainEvents());
        obj.ClearDomainEvents();
    }
}