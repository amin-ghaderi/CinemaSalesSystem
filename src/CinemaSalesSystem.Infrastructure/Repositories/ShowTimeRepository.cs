using CinemaSales.Application.Abstractions.Persistence;
using CinemaSales.Domain.Aggregates.Movies;
using CinemaSales.Infrastructure.Persistence.Context;

using Microsoft.EntityFrameworkCore;

namespace CinemaSales.Infrastructure.Repositories;

public sealed class ShowTimeRepository : IShowTimeRepository
{
    private readonly ApplicationDbContext _context;

    public ShowTimeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ShowTime?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.ShowTimes
            .AsNoTracking()
            .FirstOrDefaultAsync(st => st.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ShowTime>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.ShowTimes
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ShowTime>> GetByMovieIdAsync(
        Guid movieId,
        CancellationToken cancellationToken)
    {
        return await _context.ShowTimes
            .AsNoTracking()
            .Where(st => st.MovieId == movieId)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Matches <see cref="ShowTime.Auditorium"/> to the canonical string form of <paramref name="cinemaId"/>.
    /// Persisted data should use the same representation (e.g. <c>cinemaId.ToString()</c>).
    /// </summary>
    public async Task<IReadOnlyList<ShowTime>> GetByCinemaIdAsync(
        Guid cinemaId,
        CancellationToken cancellationToken)
    {
        var auditoriumKey = cinemaId.ToString();

        return await _context.ShowTimes
            .AsNoTracking()
            .Where(st => st.Auditorium == auditoriumKey)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ShowTime showTime, CancellationToken cancellationToken)
    {
        await _context.ShowTimes.AddAsync(showTime, cancellationToken);
    }

    public Task UpdateAsync(ShowTime showTime, CancellationToken cancellationToken)
    {
        _context.ShowTimes.Update(showTime);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var showTime = await _context.ShowTimes
            .FirstOrDefaultAsync(st => st.Id == id, cancellationToken);

        if (showTime is not null)
        {
            _context.ShowTimes.Remove(showTime);
        }
    }
}
