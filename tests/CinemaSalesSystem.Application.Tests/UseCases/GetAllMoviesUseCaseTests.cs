using CinemaSales.Application.Abstractions.Persistence;
using CinemaSales.Application.DTOs;
using CinemaSales.Application.UseCases;
using CinemaSales.Domain.Aggregates.Movies;
using CinemaSales.Domain.Enums;

using FluentAssertions;

using Moq;

namespace CinemaSalesSystem.Application.Tests.UseCases;

public class GetAllMoviesUseCaseTests
{
    private readonly Mock<IMovieRepository> _movieRepositoryMock = new();

    [Fact]
    public async Task ExecuteAsyncShouldReturnMappedDtos()
    {
        var movies = new List<Movie>
        {
            new("Alpha", 100, MovieRating.Pg13, genre: "Action"),
            new("Beta", 90, MovieRating.G, genre: "Family"),
        };
        _movieRepositoryMock
            .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(movies);

        var sut = new GetAllMoviesUseCase(_movieRepositoryMock.Object);

        IReadOnlyList<MovieDto> result = await sut.ExecuteAsync(CancellationToken.None);

        result.Should().HaveCount(2);
        result[0].Title.Should().Be("Alpha");
        result[0].Genre.Should().Be("Action");
        result[0].DurationMinutes.Should().Be(100);
        result[1].Title.Should().Be("Beta");
        _movieRepositoryMock.Verify(r => r.GetAllAsync(CancellationToken.None), Times.Once);
    }
}
