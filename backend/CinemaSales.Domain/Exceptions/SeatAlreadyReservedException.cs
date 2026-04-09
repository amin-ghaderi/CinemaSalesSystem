namespace CinemaSales.Domain.Exceptions;

/// <summary>
/// Thrown when attempting to reserve a seat that is already reserved for the same show time.
/// </summary>
public sealed class SeatAlreadyReservedException : DomainException
{
    /// <summary>
    /// Initializes a new instance of <see cref="SeatAlreadyReservedException"/>.
    /// </summary>
    /// <param name="message">The error message.</param>
    public SeatAlreadyReservedException(string message)
        : base(message)
    {
    }
}
