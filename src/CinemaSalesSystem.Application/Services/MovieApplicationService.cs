using CinemaSales.Application.Abstractions.Services;
using CinemaSales.Application.DTOs;
using CinemaSales.Application.UseCases;

namespace CinemaSales.Application.Services;

public sealed class MovieApplicationService : IMovieService
{
    private readonly GetAllMoviesUseCase _getAllMovies;
    private readonly GetMovieByIdUseCase _getMovieById;

    public MovieApplicationService(
        GetAllMoviesUseCase getAllMovies,
        GetMovieByIdUseCase getMovieById)
    {
        _getAllMovies = getAllMovies;
        _getMovieById = getMovieById;
    }

    public Task<IReadOnlyList<MovieDto>> GetAllMoviesAsync(CancellationToken cancellationToken) =>
        _getAllMovies.ExecuteAsync(cancellationToken);

    public Task<MovieDto?> GetMovieByIdAsync(Guid id, CancellationToken cancellationToken) =>
        _getMovieById.ExecuteAsync(id, cancellationToken);
}
