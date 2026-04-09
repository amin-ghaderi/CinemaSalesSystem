using CinemaSales.Domain.Aggregates.Movies;

namespace CinemaSales.Application.Abstractions.Persistence;

/// <summary>
/// Persistence contract for <see cref="Movie"/> aggregates.
/// </summary>
public interface IMovieRepository
{
    Task<Movie?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyList<Movie>> GetAllAsync(CancellationToken cancellationToken);

    Task AddAsync(Movie movie, CancellationToken cancellationToken);

    Task UpdateAsync(Movie movie, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
