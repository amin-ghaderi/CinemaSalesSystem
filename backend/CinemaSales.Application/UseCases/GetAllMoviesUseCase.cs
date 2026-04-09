using CinemaSales.Application.Abstractions.Persistence;
using CinemaSales.Application.DTOs;
using CinemaSales.Application.Mappings;
using CinemaSales.Domain.Aggregates.Movies;

namespace CinemaSales.Application.UseCases;

public sealed class GetAllMoviesUseCase
{
    private readonly IMovieRepository _movieRepository;

    public GetAllMoviesUseCase(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<IReadOnlyList<MovieDto>> ExecuteAsync(CancellationToken cancellationToken)
    {
        IReadOnlyList<Movie> movies = await _movieRepository.GetAllAsync(cancellationToken);

        return movies
            .Select(MovieMapper.ToDto)
            .ToList();
    }
}
