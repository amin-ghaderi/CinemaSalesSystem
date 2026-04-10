using CinemaSales.Application.Abstractions.Persistence;
using CinemaSales.Domain.Aggregates.Orders;
using CinemaSales.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CinemaSales.Infrastructure.Repositories;

public sealed class OrderSnackLineRepository : IOrderSnackLineRepository
{
    private readonly ApplicationDbContext _context;

    public OrderSnackLineRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<OrderSnackLine>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.OrderSnackLines
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
