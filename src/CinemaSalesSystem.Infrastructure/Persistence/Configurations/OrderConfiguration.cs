using CinemaSales.Domain.Aggregates.Orders;
using CinemaSales.Domain.ValueObjects;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaSales.Infrastructure.Persistence.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);

        builder.Ignore("_domainEvents");
        builder.Ignore(nameof(Order.Tickets));
        builder.Ignore(nameof(Order.SnackLines));

        builder.Property(o => o.CreatedAt).IsRequired();
        builder.Property(o => o.Status).HasConversion<int>().IsRequired();

        builder.Property(o => o.TotalAmount)
            .HasColumnName("TotalAmount")
            .HasMaxLength(96)
            .HasConversion(MoneyStorage.RequiredConverter);

        builder.Property<Guid?>("_appliedCampaignId")
            .HasColumnName("AppliedCampaignId");

        builder.Property<Money?>("_appliedDiscount")
            .HasColumnName("AppliedDiscount")
            .HasMaxLength(96)
            .HasConversion(MoneyStorage.NullableConverter);

        builder.HasMany(typeof(Ticket), "_tickets")
            .WithOne()
            .HasForeignKey(nameof(Ticket.OrderId))
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(typeof(OrderSnackLine), "_snackLines")
            .WithOne()
            .HasForeignKey("OrderId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
