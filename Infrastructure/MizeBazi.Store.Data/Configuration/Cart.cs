using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Data.Entities;

internal class Cart : DbCart
{
    public ICollection<CartItem> CartItems { get; set; }

}

internal class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(p => p.Id);
    }
}