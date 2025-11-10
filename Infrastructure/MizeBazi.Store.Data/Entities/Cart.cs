
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MizeBazi.Store.Common.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MizeBazi.Store.Data.Entities;

public class Cart : BaseEntity
{
    public long UserId { get; set; } 

    public ICollection<CartItem> CartItems { get; set; }

}

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(p => p.Id);
    }
}