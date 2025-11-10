using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Common.Helper;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MizeBazi.Store.Data.Entities;

public class Order : SoftDeleteEntity
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

    public ICollection<OrderItem> OrderItems { get; set; }
    public Payment Payment { get; set; }
}
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(p => p.Id);

        //var OrderNumber = Hash.GenerateOrderNumber();
        //builder.Property(e => e.OrderNumber).HasDefaultValue(OrderNumber);

        builder.Property(e => e.ShippingCity).IsRequired();
        builder.Property(e => e.ShippingAddress).IsRequired();
        builder.Property(e => e.ShippingPostalCode).IsRequired();

        builder.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");

    }
}