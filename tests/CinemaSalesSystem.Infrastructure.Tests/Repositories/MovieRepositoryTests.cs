using CinemaSales.Domain.Aggregates.Movies;
using CinemaSales.Domain.Enums;
using CinemaSales.Infrastructure.Repositories;

using CinemaSalesSystem.Infrastructure.Tests.Common;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;

namespace CinemaSalesSystem.Infrastructure.Tests.Repositories;

public class MovieRepositoryTests
{
    [Fact]
    public async Task AddAsyncShouldAddMovieToDatabase()
    {
        await using var context = TestDbContextFactory.Create();
        var repository = new MovieRepository(context);

        var movie = new Movie("Interstellar", 169, MovieRating.Pg13, genre: "Sci-Fi");

        await repository.AddAsync(movie, CancellationToken.None);
        await context.SaveChangesAsync();

        var result = await context.Movies.AsNoTracking().SingleOrDefaultAsync(m => m.Id == movie.Id);

        result.Should().NotBeNull();
        result!.Title.Should().Be("Interstellar");
        result.DurationMinutes.Should().Be(169);
        result.Genre.Should().Be("Sci-Fi");
        result.Rating.Should().Be(MovieRating.Pg13);
    }

    [Fact]
    public async Task GetAllAsyncShouldReturnAllMovies()
    {
        await using var context = TestDbContextFactory.Create();
        var repository = new MovieRepository(context);

        await repository.AddAsync(
            new Movie("Inception", 148, MovieRating.Pg13, genre: "Sci-Fi"),
            CancellationToken.None);
        await repository.AddAsync(
            new Movie("The Dark Knight", 152, MovieRating.Pg13, genre: "Action"),
            CancellationToken.None);
        await context.SaveChangesAsync();

        var movies = await repository.GetAllAsync(CancellationToken.None);

        movies.Should().HaveCount(2);
        movies.Select(m => m.Title).Should().BeEquivalentTo(["Inception", "The Dark Knight"]);
    }

    [Fact]
    public async Task GetByIdAsyncShouldReturnMovieWhenPresent()
    {
        await using var context = TestDbContextFactory.Create();
        var repository = new MovieRepository(context);

        var movie = new Movie("Dune", 155, MovieRating.Pg13, genre: "Sci-Fi");
        await repository.AddAsync(movie, CancellationToken.None);
        await context.SaveChangesAsync();

        var result = await repository.GetByIdAsync(movie.Id, CancellationToken.None);

        result.Should().NotBeNull();
        result!.Id.Should().Be(movie.Id);
        result.Title.Should().Be("Dune");
    }
}
