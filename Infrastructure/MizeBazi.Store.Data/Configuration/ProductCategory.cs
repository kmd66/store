using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Data.Entities;

internal class ProductCategory: DbProductCategory
{
    public Product Product { get; set; }
    public Category Category { get; set; }
}

internal class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
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