using CinemaSales.Domain.Common;

namespace CinemaSales.Domain.ValueObjects;

/// <summary>
/// Represents a non-negative monetary amount in a specific currency.
/// </summary>
public sealed record Money
{
    /// <summary>
    /// Initializes a new instance of <see cref="Money"/>.
    /// </summary>
    /// <param name="amount">The amount; must be non-negative.</param>
    /// <param name="currency">The ISO-like currency code (e.g. EUR).</param>
    public Money(decimal amount, string currency)
    {
        Guard.AgainstNegative(amount, nameof(amount));
        Amount = decimal.Round(amount, 2, MidpointRounding.AwayFromZero);
        Currency = Guard.AgainstNullOrWhiteSpace(currency, nameof(currency)).Trim().ToUpperInvariant();
    }

    /// <summary>
    /// Gets the monetary amount.
    /// </summary>
    public decimal Amount { get; }

    /// <summary>
    /// Gets the currency code.
    /// </summary>
    public string Currency { get; }

    /// <summary>
    /// Adds two money values with matching currency.
    /// </summary>
    public static Money operator +(Money left, Money right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);
        EnsureSameCurrency(left, right);
        return new Money(left.Amount + right.Amount, left.Currency);
    }

    /// <summary>
    /// Subtracts two money values with matching currency.
    /// </summary>
    public static Money operator -(Money left, Money right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);
        EnsureSameCurrency(left, right);
        return new Money(left.Amount - right.Amount, left.Currency);
    }

    /// <summary>
    /// Multiplies an amount by a non-negative factor.
    /// </summary>
    public static Money Multiply(Money money, decimal factor)
    {
        ArgumentNullException.ThrowIfNull(money);
        Guard.AgainstNegative(factor, nameof(factor));
        return new Money(decimal.Round(money.Amount * factor, 2, MidpointRounding.AwayFromZero), money.Currency);
    }

    /// <summary>
    /// Returns zero in the specified currency.
    /// </summary>
    public static Money Zero(string currency) => new(0, currency);

    private static void EnsureSameCurrency(Money left, Money right)
    {
        if (!string.Equals(left.Currency, right.Currency, StringComparison.Ordinal))
        {
            throw new InvalidOperationException($"Currency mismatch: {left.Currency} vs {right.Currency}.");
        }
    }
}
