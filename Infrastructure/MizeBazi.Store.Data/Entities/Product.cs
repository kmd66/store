using System.ComponentModel.DataAnnotations;
using MizeBazi.Store.Common.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MizeBazi.Store.Data.Entities;
public class Product : SoftDeleteEntity
{
    public string Name { get; set; }
    public string Images { get; set; } // JSON array of image URLs

    public decimal Price { get; set; }

    public decimal CompareAtPrice { get; set; }

    public string Description { get; set; }

    public int StockQuantity { get; set; } //موجودی انبار

    public string SKU { get; set; } // کد کالا

    public bool IsPublished { get; set; } //وضعیت انتشار

    // Brand - یک برند
    public long BrandId { get; set; }

    public Brand Brand { get; set; }

    // دسته‌بندی‌ها - چند به چند
    public ICollection<ProductCategory> ProductCategories { get; set; }
    public ICollection<CartItem> CartItems { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}

public class ProductConfiguration : IEntityTypeConfiguration<Product>
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