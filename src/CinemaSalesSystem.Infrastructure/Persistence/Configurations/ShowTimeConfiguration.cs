using CinemaSales.Domain.Aggregates.Movies;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaSales.Infrastructure.Persistence.Configurations;

internal sealed class ShowTimeConfiguration : IEntityTypeConfiguration<ShowTime>
{
    public void Configure(EntityTypeBuilder<ShowTime> builder)
    {
        builder.ToTable("ShowTimes");

        builder.HasKey(st => st.Id);

        builder.Property(st => st.MovieId).IsRequired();
        builder.Property(st => st.StartTime).IsRequired();
        builder.Property(st => st.Auditorium).HasMaxLength(128).IsRequired();
        builder.Property(st => st.Slot).HasConversion<int>().IsRequired();
    }
}
