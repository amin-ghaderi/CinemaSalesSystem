namespace CinemaSales.Domain.Exceptions;

/// <summary>
/// Base exception for all domain rule violations.
/// </summary>
public abstract class DomainException : Exception
{
    /// <summary>
    /// Initializes a new instance of <see cref="DomainException"/>.
    /// </summary>
    /// <param name="message">The error message.</param>
    protected DomainException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="DomainException"/>.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    protected DomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
