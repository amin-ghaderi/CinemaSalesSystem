using CinemaSales.Domain.Aggregates.Movies;

namespace CinemaSales.Application.Abstractions.Persistence;

/// <summary>
/// Persistence contract for <see cref="ShowTime"/> entities.
/// </summary>
public interface IShowTimeRepository
{
    Task<ShowTime?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyList<ShowTime>> GetAllAsync(CancellationToken cancellationToken);

    Task<IReadOnlyList<ShowTime>> GetByMovieIdAsync(Guid movieId, CancellationToken cancellationToken);

    Task<IReadOnlyList<ShowTime>> GetByCinemaIdAsync(Guid cinemaId, CancellationToken cancellationToken);

    Task AddAsync(ShowTime showTime, CancellationToken cancellationToken);

    Task UpdateAsync(ShowTime showTime, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
