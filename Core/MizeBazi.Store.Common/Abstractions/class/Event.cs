using MizeBazi.Store.Common.Abstractions;

namespace MizeBazi.Store.Common.Shared;

public class EventDispatcher(IServiceProvider serviceProvider) : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public Task DispatchLocalAsync(DomainEvent domainEvent)
    {
        throw new NotImplementedException();
    }

    public Task DispatchToBusAsync(DataSyncEvent model)
    {
        //IMessageBus messageBus
        throw new NotImplementedException();
    }
}