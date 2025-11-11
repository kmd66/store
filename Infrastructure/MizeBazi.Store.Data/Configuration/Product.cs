using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Data.Entities;
internal class Product : DbProduct
{
    public Brand Brand { get; set; }
    public ICollection<ProductCategory> ProductCategories { get; set; }
    public ICollection<CartItem> CartItems { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Images).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Images).IsRequired();
        builder.Property(p => p.Images).HasDefaultValue("[]");

        builder.Property(e => e.Price).HasColumnType("decimal(18,2)");
        builder.Property(e => e.CompareAtPrice).HasColumnType("decimal(18,2)");

        builder.Property(p => p.IsPublished).HasDefaultValue(true);

        builder.HasOne(ci => ci.Brand)
               .WithMany(c => c.Products)
               .HasForeignKey(ci => ci.BrandId);

    }
}