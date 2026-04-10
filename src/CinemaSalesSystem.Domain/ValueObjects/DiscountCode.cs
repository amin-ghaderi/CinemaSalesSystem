using CinemaSales.Domain.Common;

namespace CinemaSales.Domain.ValueObjects;

/// <summary>
/// Represents a customer-entered promotional code.
/// </summary>
public sealed record DiscountCode
{
    /// <summary>
    /// Initializes a new instance of <see cref="DiscountCode"/>.
    /// </summary>
    /// <param name="code">The raw code value.</param>
    public DiscountCode(string code)
    {
        Code = Guard.AgainstNullOrWhiteSpace(code, nameof(code)).Trim().ToUpperInvariant();
    }

    /// <summary>
    /// Gets the normalized discount code.
    /// </summary>
    public string Code { get; }
}
