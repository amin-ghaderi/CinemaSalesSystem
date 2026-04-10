using CinemaSales.Application.Abstractions.Persistence;
using CinemaSales.Domain.Entities;
using CinemaSales.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CinemaSales.Infrastructure.Repositories;

public sealed class SnackRepository : ISnackRepository
{
    private readonly ApplicationDbContext _context;

    public SnackRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Snack?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Snacks
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Snack>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Snacks
            .AsNoTracking()
            .OrderBy(s => s.Name)
            .ToListAsync(cancellationToken);
    }
}
