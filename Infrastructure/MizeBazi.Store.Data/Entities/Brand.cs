using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MizeBazi.Store.Common.Shared;
using System.ComponentModel.DataAnnotations;

namespace MizeBazi.Store.Data.Entities;

public class Brand : SoftDeleteEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string LogoUrl { get; set; }

    public ICollection<Product> Products { get; set; }
}

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
    }
}
