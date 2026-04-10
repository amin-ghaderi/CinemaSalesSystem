using CinemaSales.Application.Abstractions.Persistence;
using CinemaSales.Domain.Aggregates.Movies;
using CinemaSales.Infrastructure.Persistence.Context;

using Microsoft.EntityFrameworkCore;

namespace CinemaSales.Infrastructure.Repositories;

public sealed class MovieRepository : IMovieRepository
{
    private readonly ApplicationDbContext _context;

    public MovieRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Movie?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Movies
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Movie>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Movies
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Movie movie, CancellationToken cancellationToken)
    {
        await _context.Movies.AddAsync(movie, cancellationToken);
    }

    public Task UpdateAsync(Movie movie, CancellationToken cancellationToken)
    {
        _context.Movies.Update(movie);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var movie = await _context.Movies
            .Include("_showTimes")
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

        if (movie is not null)
        {
            _context.Movies.Remove(movie);
        }
    }
}
