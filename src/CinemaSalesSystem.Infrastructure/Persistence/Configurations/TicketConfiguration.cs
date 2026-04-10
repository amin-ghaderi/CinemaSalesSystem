using CinemaSales.Domain.Aggregates.Movies;
using CinemaSales.Domain.Aggregates.Orders;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaSales.Infrastructure.Persistence.Configurations;

internal sealed class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Tickets");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.OrderId).IsRequired();
        builder.Property(t => t.ShowTimeId).IsRequired();
        builder.Property(t => t.TicketType).HasConversion<int>().IsRequired();

        builder.Property(t => t.SeatNumber)
            .HasColumnName("SeatNumberValue")
            .HasMaxLength(32)
            .HasConversion(SeatNumberStorage.Converter);

        builder.Property(t => t.Price)
            .HasColumnName("Price")
            .HasMaxLength(96)
            .HasConversion(MoneyStorage.RequiredConverter);

        builder.HasOne<ShowTime>()
            .WithMany()
            .HasForeignKey(t => t.ShowTimeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
