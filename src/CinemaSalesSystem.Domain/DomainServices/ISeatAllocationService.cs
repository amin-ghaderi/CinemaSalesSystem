using CinemaSales.Domain.Aggregates.Orders;
using CinemaSales.Domain.ValueObjects;

namespace CinemaSales.Domain.DomainServices;

/// <summary>
/// Enforces seat reservation rules for show times.
/// </summary>
public interface ISeatAllocationService
{
    /// <summary>
    /// Ensures no existing ticket already claims the same seat for the show time.
    /// </summary>
    /// <param name="showTimeId">Show time identifier.</param>
    /// <param name="seatNumber">Seat to reserve.</param>
    /// <param name="existingTickets">Tickets already issued (any order) for the same screening.</param>
    void EnsureSeatAvailableForShowTime(
        Guid showTimeId,
        SeatNumber seatNumber,
        IEnumerable<Ticket> existingTickets);
}
