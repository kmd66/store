using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Common.Helper;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MizeBazi.Store.Data.Entities;

internal class Order : DbOrder
{
    public ICollection<OrderItem> OrderItems { get; set; }
    public Payment Payment { get; set; }
}
internal class OrderConfiguration : IEntityTypeConfiguration<Order>
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