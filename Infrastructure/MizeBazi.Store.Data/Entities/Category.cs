using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Data.Entities;

public class Category : SoftDeleteEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string ImageUrl { get; set; }

    public ICollection<ProductCategory> ProductCategories { get; set; }
}
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
    }
}