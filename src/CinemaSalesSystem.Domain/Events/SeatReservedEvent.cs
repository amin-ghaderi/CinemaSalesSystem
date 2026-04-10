using CinemaSales.Domain.Common;

namespace CinemaSales.Domain.Events;

/// <summary>
/// Raised when a physical seat is marked reserved for a screening.
/// </summary>
/// <param name="ShowTimeId">The show time identifier.</param>
/// <param name="SeatRow">The seat row.</param>
/// <param name="SeatNumber">The seat number within the row.</param>
public sealed record SeatReservedEvent(
    Guid ShowTimeId,
    string SeatRow,
    int SeatNumber) : BaseDomainEvent;
