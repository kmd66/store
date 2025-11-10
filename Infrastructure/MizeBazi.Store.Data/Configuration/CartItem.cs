using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Data.Entities;

internal class CartItem : DbCartItem
{
    public Cart Cart { get; set; }
    public Product Product { get; set; }
}

internal class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(ci => new { ci.CartId , ci.ProductId });

        builder.HasOne(ci => ci.Cart)
               .WithMany(c => c.CartItems)
               .HasForeignKey(ci => ci.CartId);

        builder.HasOne(ci => ci.Product)
               .WithMany(c => c.CartItems)
               .HasForeignKey(ci => ci.ProductId);
    }
}