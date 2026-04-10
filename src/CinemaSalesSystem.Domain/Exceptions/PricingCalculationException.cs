namespace CinemaSales.Domain.Exceptions;

/// <summary>
/// Thrown when pricing or totals cannot be computed due to inconsistent domain state.
/// </summary>
public sealed class PricingCalculationException : DomainException
{
    /// <summary>
    /// Initializes a new instance of <see cref="PricingCalculationException"/>.
    /// </summary>
    /// <param name="message">The error message.</param>
    public PricingCalculationException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="PricingCalculationException"/>.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public PricingCalculationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
