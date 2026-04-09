using CinemaSales.Application.Abstractions.Persistence;
using CinemaSales.Domain.Aggregates.Orders;
using CinemaSales.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CinemaSales.Infrastructure.Repositories;

public sealed class TicketRepository : ITicketRepository
{
    private readonly ApplicationDbContext _context;

    public TicketRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Ticket?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Tickets
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Ticket>> GetByShowTimeIdAsync(
        Guid showTimeId,
        CancellationToken cancellationToken)
    {
        return await _context.Tickets
            .AsNoTracking()
            .Where(t => t.ShowTimeId == showTimeId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Ticket ticket, CancellationToken cancellationToken)
    {
        await _context.Tickets.AddAsync(ticket, cancellationToken);
    }
}
