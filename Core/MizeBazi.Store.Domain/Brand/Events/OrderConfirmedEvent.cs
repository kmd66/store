using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Domain;

public class OrderConfirmedEvent: DomainEvent
{
    public Guid OrderId { get; protected set; }

    public OrderConfirmedEvent(Guid orderId, EventType t)
    {
        OrderId = orderId;
    }
}
