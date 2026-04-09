using CinemaSales.Domain.Aggregates.Orders;
using CinemaSales.Domain.Entities;
using CinemaSales.Domain.Enums;
using CinemaSales.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaSales.Infrastructure.Persistence.Configurations;

internal sealed class OrderSnackLineConfiguration : IEntityTypeConfiguration<OrderSnackLine>
{
    public void Configure(EntityTypeBuilder<OrderSnackLine> builder)
    {
        builder.ToTable("OrderSnackLines");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.SnackId).IsRequired();
        builder.Property(l => l.Quantity).IsRequired();
        builder.Property(l => l.VatType).HasConversion<int>().IsRequired();

        builder.Property<Guid>("OrderId").IsRequired();

        builder.Property(l => l.UnitPrice)
            .HasColumnName("UnitPrice")
            .HasMaxLength(96)
            .HasConversion(MoneyStorage.RequiredConverter);

        builder.HasOne<Snack>()
            .WithMany()
            .HasForeignKey(l => l.SnackId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
