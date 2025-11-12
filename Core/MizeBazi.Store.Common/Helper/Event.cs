using MizeBazi.Store.Common.Abstractions;

namespace MizeBazi.Store.Common.Shared;

public class EventDispatcher(IServiceProvider serviceProvider) : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task DispatchLocalAsync<TEvent>(TEvent domainEvent)
    {
        var t = typeof(TEvent);
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

    public async Task ExecuteEvents(Entity obj, EventType t)
    {
        if (obj == null) return;
        obj.Confirm(t);
        var domainEvents = obj.DomainEvents();
        foreach (var domainEvent in domainEvents)
        {
            dynamic dynamicEvent = domainEvent;
            await _dispatcher.DispatchLocalAsync(dynamicEvent);
        }
        obj.ClearDomainEvents();
    }

}