
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Common.Abstractions;

public interface IEventDispatcher
{
    /// <summary>
    /// اطلاع‌رسانی به هندلرهای داخلی 
    /// </summary>
    Task DispatchLocalAsync<TEvent>(TEvent domainEvent);

    /// <summary>
    /// ارسال رویداد به سرویس‌های دیگر
    /// </summary>
    Task DispatchToBusAsync(DataSyncEvent model);
}

public abstract class DomainEvent
{
    public DateTime Date { get; } = DateTime.UtcNow;
}
public interface IDomainEventHandler<T>
{
    Task Handle(T domainEvent);
}