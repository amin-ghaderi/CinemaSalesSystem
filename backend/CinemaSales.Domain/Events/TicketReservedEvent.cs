using CinemaSales.Domain.Common;

namespace CinemaSales.Domain.Events;

/// <summary>
/// Raised when a ticket line is added to an order for a show time and seat.
/// </summary>
/// <param name="OrderId">The order identifier.</param>
/// <param name="ShowTimeId">The show time identifier.</param>
/// <param name="SeatRow">The seat row.</param>
/// <param name="SeatNumber">The seat number within the row.</param>
public sealed record TicketReservedEvent(
    Guid OrderId,
    Guid ShowTimeId,
    string SeatRow,
    int SeatNumber) : BaseDomainEvent;
