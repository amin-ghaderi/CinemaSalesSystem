namespace CinemaSales.Domain.Exceptions;

/// <summary>
/// Thrown when a discount cannot be applied (e.g. exceeds order total or invalid configuration).
/// </summary>
public sealed class InvalidDiscountException : DomainException
{
    /// <summary>
    /// Initializes a new instance of <see cref="InvalidDiscountException"/>.
    /// </summary>
    /// <param name="message">The error message.</param>
    public InvalidDiscountException(string message)
        : base(message)
    {
    }
}
