using CinemaSales.Domain.Enums;

namespace CinemaSales.Domain.ValueObjects;

/// <summary>
/// Associates a VAT type with a fractional rate (e.g. 0.24m for 24%).
/// </summary>
public sealed record VatRate
{
    /// <summary>
    /// Initializes a new instance of <see cref="VatRate"/>.
    /// </summary>
    /// <param name="rate">The VAT rate as a fraction between 0 and 1 inclusive.</param>
    /// <param name="vatType">The VAT classification.</param>
    public VatRate(decimal rate, VatType vatType)
    {
        if (rate is < 0 or > 1)
        {
            throw new ArgumentOutOfRangeException(nameof(rate), rate, "VAT rate must be between 0 and 1.");
        }

        Rate = rate;
        VatType = vatType;
    }

    /// <summary>
    /// Gets the fractional VAT rate.
    /// </summary>
    public decimal Rate { get; }

    /// <summary>
    /// Gets the VAT classification.
    /// </summary>
    public VatType VatType { get; }
}
