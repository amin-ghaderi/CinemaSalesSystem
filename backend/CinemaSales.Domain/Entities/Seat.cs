using CinemaSales.Domain.Common;
using CinemaSales.Domain.Exceptions;
using CinemaSales.Domain.ValueObjects;

namespace CinemaSales.Domain.Entities;

/// <summary>
/// A seat instance for a specific show time.
/// </summary>
public sealed class Seat : Entity
{
    /// <summary>
    /// Initializes a new seat for a show time.
    /// </summary>
    /// <param name="showTimeId">Owning show time.</param>
    /// <param name="seatNumber">Seat coordinates.</param>
    public Seat(Guid showTimeId, SeatNumber seatNumber)
        : base()
    {
        ShowTimeId = Guard.AgainstEmpty(showTimeId, nameof(showTimeId));
        SeatNumber = Guard.AgainstNull(seatNumber, nameof(seatNumber));
    }

    /// <summary>
    /// Initializes a seat with a known identifier.
    /// </summary>
    public Seat(Guid id, Guid showTimeId, SeatNumber seatNumber, bool isReserved)
        : base(id)
    {
        ShowTimeId = Guard.AgainstEmpty(showTimeId, nameof(showTimeId));
        SeatNumber = Guard.AgainstNull(seatNumber, nameof(seatNumber));
        IsReserved = isReserved;
    }

    /// <summary>
    /// Gets the show time identifier.
    /// </summary>
    public Guid ShowTimeId { get; private set; }

    /// <summary>
    /// Gets the seat coordinates.
    /// </summary>
    public SeatNumber SeatNumber { get; private set; }

    /// <summary>
    /// Gets whether the seat is currently reserved.
    /// </summary>
    public bool IsReserved { get; private set; }

    /// <summary>
    /// Marks the seat as reserved.
    /// </summary>
    /// <exception cref="SeatAlreadyReservedException">When already reserved.</exception>
    public void Reserve()
    {
        if (IsReserved)
        {
            throw new SeatAlreadyReservedException($"Seat {SeatNumber} is already reserved for show time {ShowTimeId}.");
        }

        IsReserved = true;
    }

    /// <summary>
    /// Releases the reservation.
    /// </summary>
    /// <exception cref="InvalidSeatException">When the seat is not reserved.</exception>
    public void Release()
    {
        if (!IsReserved)
        {
            throw new InvalidSeatException($"Seat {SeatNumber} is not reserved.");
        }

        IsReserved = false;
    }
}
