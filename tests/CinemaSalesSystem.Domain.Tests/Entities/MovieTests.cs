using CinemaSales.Domain.Aggregates.Movies;
using CinemaSales.Domain.Enums;

using FluentAssertions;

namespace CinemaSalesSystem.Domain.Tests.Entities;

/// <summary>
/// Tests for <see cref="Movie"/> (domain aggregate under CinemaSales.Domain.Aggregates.Movies).
/// </summary>
public class MovieTests
{
    [Fact]
    public void ConstructorShouldSetValidProperties()
    {
        var movie = new Movie("Inception", 148, MovieRating.Pg13, description: "Mind heist", genre: "Sci-Fi");

        movie.Title.Should().Be("Inception");
        movie.Genre.Should().Be("Sci-Fi");
        movie.DurationMinutes.Should().Be(148);
        movie.Rating.Should().Be(MovieRating.Pg13);
        movie.Description.Should().Be("Mind heist");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void ConstructorShouldRejectEmptyTitle(string title)
    {
        var act = () => new Movie(title, 120, MovieRating.G);

        act.Should().Throw<ArgumentException>()
            .WithParameterName(nameof(title));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ConstructorShouldRejectNonPositiveDuration(int minutes)
    {
        var act = () => new Movie("Title", minutes, MovieRating.G);

        act.Should().Throw<ArgumentException>()
            .WithParameterName("durationMinutes");
    }
}
