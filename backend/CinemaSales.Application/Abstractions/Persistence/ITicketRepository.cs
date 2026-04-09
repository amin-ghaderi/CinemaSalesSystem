using CinemaSales.Domain.Aggregates.Orders;

namespace CinemaSales.Application.Abstractions.Persistence;

/// <summary>
/// Persistence contract for <see cref="Ticket"/> entities (order ticket lines).
/// </summary>
public interface ITicketRepository
{
    Task<Ticket?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyList<Ticket>> GetByShowTimeIdAsync(Guid showTimeId, CancellationToken cancellationToken);

    Task AddAsync(Ticket ticket, CancellationToken cancellationToken);
}
