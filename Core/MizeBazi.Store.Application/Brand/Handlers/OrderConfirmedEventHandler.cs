 using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application;

public class OrderConfirmedEventHandler : IDomainEventHandler<OrderConfirmedEvent>
{
    public Task Handle(OrderConfirmedEvent domainEvent)
    {
        Console.WriteLine($"[Handler] Order {domainEvent.OrderId} confirmed at {domainEvent.Date}");
        return Task.CompletedTask;
    }
}
