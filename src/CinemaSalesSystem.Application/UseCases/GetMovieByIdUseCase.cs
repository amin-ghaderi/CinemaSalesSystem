using CinemaSales.Application.Abstractions.Persistence;
using CinemaSales.Application.DTOs;
using CinemaSales.Application.Mappings;
using CinemaSales.Domain.Aggregates.Movies;

namespace CinemaSales.Application.UseCases;

public sealed class GetMovieByIdUseCase
{
    private readonly IMovieRepository _movieRepository;

    public GetMovieByIdUseCase(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<MovieDto?> ExecuteAsync(Guid id, CancellationToken cancellationToken)
    {
        Movie? movie = await _movieRepository.GetByIdAsync(id, cancellationToken);

        if (movie is null)
        {
            return null;
        }

        return MovieMapper.ToDto(movie);
    }
}
