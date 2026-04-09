using CinemaSales.Domain.Aggregates.Orders;
using CinemaSales.Domain.Exceptions;
using CinemaSales.Domain.ValueObjects;

namespace CinemaSales.Domain.DomainServices;

/// <inheritdoc />
public sealed class SeatAllocationService : ISeatAllocationService
{
    /// <inheritdoc />
    public void EnsureSeatAvailableForShowTime(
        Guid showTimeId,
        SeatNumber seatNumber,
        IEnumerable<Ticket> existingTickets)
    {
        ArgumentNullException.ThrowIfNull(seatNumber);
        ArgumentNullException.ThrowIfNull(existingTickets);

        foreach (var ticket in existingTickets)
        {
            if (ticket.ShowTimeId == showTimeId && ticket.SeatNumber == seatNumber)
            {
                throw new SeatAlreadyReservedException(
                    $"Seat {seatNumber} is already reserved for show time {showTimeId}.");
            }
        }
    }
}
