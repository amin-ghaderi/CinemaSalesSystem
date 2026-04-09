using CinemaSales.Domain.Aggregates.Movies;
using CinemaSales.Domain.Entities;
using CinemaSales.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaSales.Infrastructure.Persistence.Configurations;

internal sealed class SeatConfiguration : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        builder.ToTable("Seats");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.ShowTimeId).IsRequired();
        builder.Property(s => s.IsReserved).IsRequired();

        builder.Property(s => s.SeatNumber)
            .HasColumnName("SeatNumberValue")
            .HasMaxLength(32)
            .HasConversion(SeatNumberStorage.Converter);

        builder.HasOne<ShowTime>()
            .WithMany()
            .HasForeignKey(s => s.ShowTimeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
