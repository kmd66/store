
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Common.Abstractions;

public interface IMessageBus
{
    Task PullAsync(DataSyncEvent model);
   
    Task PushAsync(DataSyncEvent model);
}
