using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MizeBazi.Store.Data.Entities;
public class OrderItem 
{
    public long OrderId { get; set; }

    public long ProductId { get; set; }

    public int Quantity { get; set; } //تعداد واحدهای یک محصول در سفارش

    public decimal Price { get; set; } // قیمت در لحظه خرید

    public Order Order { get; set; }
    public Product Product { get; set; }
}

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.Property(e => e.Price).HasColumnType("decimal(18,2)");

        builder.HasKey(ci => new { ci.OrderId , ci.ProductId });

        builder.HasOne(ci => ci.Order)
               .WithMany(c => c.OrderItems)
               .HasForeignKey(ci => ci.OrderId);

        builder.HasOne(ci => ci.Product)
               .WithMany(c => c.OrderItems)
               .HasForeignKey(ci => ci.OrderId);
    }
}