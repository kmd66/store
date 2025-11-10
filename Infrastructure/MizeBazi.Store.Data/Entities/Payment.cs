
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Data.Entities;

public class Payment : SoftDeleteEntity
{

    public long OrderId { get; set; }

    public decimal Amount { get; set; }

    public Order Order { get; set; }

}
public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(e => e.Amount).HasColumnType("decimal(18,2)");
    }
}