using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Data.Entities;

internal class Payment : DbPayment
{
    public Order Order { get; set; }

}
internal class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(e => e.Amount).HasColumnType("decimal(18,2)");
    }
}