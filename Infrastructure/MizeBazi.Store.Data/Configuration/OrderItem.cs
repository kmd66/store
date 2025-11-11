using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Data.Entities;
internal class OrderItem : DbOrderItem
{
    public Order Order { get; set; }
    public Product Product { get; set; }
}

internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
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