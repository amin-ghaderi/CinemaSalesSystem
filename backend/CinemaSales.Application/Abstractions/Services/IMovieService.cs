using CinemaSales.Application.DTOs;

namespace CinemaSales.Application.Abstractions.Services;

public interface IMovieService
{
    Task<IReadOnlyList<MovieDto>> GetAllMoviesAsync(CancellationToken cancellationToken);

    Task<MovieDto?> GetMovieByIdAsync(Guid id, CancellationToken cancellationToken);
}
