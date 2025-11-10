using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MizeBazi.Store.Data.Entities;

public class ProductCategory
{
    public long ProductId { get; set; }
    public long CategoryId { get; set; }

    public Product Product { get; set; }
    public Category Category { get; set; }
}

public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.HasKey(ci => new { ci.ProductId, ci.CategoryId });

        builder.HasOne(pc => pc.Product)
               .WithMany(p => p.ProductCategories)
               .HasForeignKey(pc => pc.ProductId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pc => pc.Category)
               .WithMany(c => c.ProductCategories)
               .HasForeignKey(pc => pc.CategoryId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}