 using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application;

public class OrderConfirmedEventHandler(IAppLogger<OrderConfirmedEventHandler> logger) : IDomainEventHandler<OrderConfirmedEvent>
{
    private readonly IAppLogger<OrderConfirmedEventHandler> _logger = logger;
    public Task Handle(OrderConfirmedEvent domainEvent)
    {
        _logger.LogError($"Handle : {domainEvent.Date}");
        Console.WriteLine($"[Handler] Order {domainEvent.OrderId} confirmed at {domainEvent.Date}");
        return Task.CompletedTask;
    }
}
