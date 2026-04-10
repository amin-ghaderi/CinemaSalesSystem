namespace CinemaSales.Domain.Exceptions;

/// <summary>
/// Thrown when a seat identifier or reservation request is invalid.
/// </summary>
public sealed class InvalidSeatException : DomainException
{
    /// <summary>
    /// Initializes a new instance of <see cref="InvalidSeatException"/>.
    /// </summary>
    /// <param name="message">The error message.</param>
    public InvalidSeatException(string message)
        : base(message)
    {
    }
}
