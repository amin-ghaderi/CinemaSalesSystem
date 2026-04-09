using CinemaSales.Application.DTOs;

namespace CinemaSales.Application.Abstractions.Services;

public interface IShowTimeService
{
    Task<IReadOnlyList<ShowTimeDto>> GetShowTimesByMovieIdAsync(Guid movieId, CancellationToken cancellationToken);

    Task<IReadOnlyList<ShowTimeDto>> GetShowTimesByCinemaIdAsync(Guid cinemaId, CancellationToken cancellationToken);
}
