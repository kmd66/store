using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Abstractions;

namespace MizeBazi.Store.Services;

public class RabbitMQMessageBus : IMessageBus
{
    public Task PullAsync(DataSyncEvent model)
    {
        return Task.CompletedTask;
    }

    public Task PushAsync(DataSyncEvent model)
    {
        return Task.CompletedTask;
    }
}
