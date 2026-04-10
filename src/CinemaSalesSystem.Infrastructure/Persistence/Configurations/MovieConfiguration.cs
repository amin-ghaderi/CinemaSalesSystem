using CinemaSales.Domain.Aggregates.Movies;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaSales.Infrastructure.Persistence.Configurations;

internal sealed class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movies");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Title).HasMaxLength(512).IsRequired();
        builder.Property(m => m.Genre).HasMaxLength(128).IsRequired();
        builder.Property(m => m.DurationMinutes).IsRequired();
        builder.Property(m => m.Rating).HasConversion<int>().IsRequired();
        builder.Property(m => m.Description).HasMaxLength(4000).IsRequired();

        builder.Ignore("_domainEvents");
        builder.Ignore(nameof(Movie.ShowTimes));

        builder.HasMany(typeof(ShowTime), "_showTimes")
            .WithOne()
            .HasForeignKey(nameof(ShowTime.MovieId))
            .OnDelete(DeleteBehavior.Cascade);
    }
}
