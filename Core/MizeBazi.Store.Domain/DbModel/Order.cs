using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Common.Helper;

namespace MizeBazi.Store.Data;

public class DbOrder : SoftDeleteEntity
{
    public string OrderNumber { get; set; } = Hash.GenerateOrderNumber();
    public long UserId { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerPhone { get; set; }

    public decimal TotalAmount { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

    // آدرس سفارش
    public string ShippingAddress { get; set; }
    public string ShippingCity { get; set; }
    public string ShippingPostalCode { get; set; }

}