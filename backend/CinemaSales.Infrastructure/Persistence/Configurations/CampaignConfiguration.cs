using CinemaSales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaSales.Infrastructure.Persistence.Configurations;

internal sealed class CampaignConfiguration : IEntityTypeConfiguration<Campaign>
{
    public void Configure(EntityTypeBuilder<Campaign> builder)
    {
        builder.ToTable("Campaigns");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).HasMaxLength(256).IsRequired();
        builder.Property(c => c.DiscountPercentage).HasPrecision(5, 2).IsRequired();
        builder.Property(c => c.DiscountCode).HasMaxLength(64).IsRequired();
        builder.Property(c => c.ValidUntil).IsRequired();
    }
}
