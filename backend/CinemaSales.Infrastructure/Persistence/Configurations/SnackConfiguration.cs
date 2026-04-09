using CinemaSales.Domain.Entities;
using CinemaSales.Domain.Enums;
using CinemaSales.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaSales.Infrastructure.Persistence.Configurations;

internal sealed class SnackConfiguration : IEntityTypeConfiguration<Snack>
{
    public void Configure(EntityTypeBuilder<Snack> builder)
    {
        builder.ToTable("Snacks");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name).HasMaxLength(256).IsRequired();
        builder.Property(s => s.VatType).HasConversion<int>().IsRequired();

        builder.Property(s => s.Price)
            .HasColumnName("Price")
            .HasMaxLength(96)
            .HasConversion(MoneyStorage.RequiredConverter);
    }
}
