using CinemaSales.Domain.Aggregates.Orders;
using CinemaSales.Domain.Common;
using CinemaSales.Domain.Enums;
using CinemaSales.Domain.Exceptions;
using CinemaSales.Domain.ValueObjects;

namespace CinemaSales.Domain.DomainServices;

/// <inheritdoc />
public sealed class VatCalculationService : IVatCalculationService
{
    private readonly IReadOnlyDictionary<VatType, decimal> _rates;

    /// <summary>
    /// Initializes the service with optional custom VAT fractions per <see cref="VatType"/> (0–1).
    /// </summary>
    /// <param name="rates">Optional overrides; defaults to cinema-friendly reduced/standard rates.</param>
    public VatCalculationService(IReadOnlyDictionary<VatType, decimal>? rates = null)
    {
        _rates = rates ?? DefaultRates;
    }

    private static IReadOnlyDictionary<VatType, decimal> DefaultRates { get; } =
        new Dictionary<VatType, decimal>
        {
            [VatType.None] = 0m,
            [VatType.Reduced] = 0.10m,
            [VatType.Standard] = 0.24m
        };

    /// <inheritdoc />
    public Money CalculateVatForSnackLines(IReadOnlyCollection<OrderSnackLine> lines, string currency)
    {
        Guard.AgainstNullOrWhiteSpace(currency, nameof(currency));
        ArgumentNullException.ThrowIfNull(lines);

        if (lines.Count == 0)
        {
            return Money.Zero(currency);
        }

        Money sum = Money.Zero(currency);
        foreach (var line in lines)
        {
            if (!string.Equals(line.UnitPrice.Currency, currency, StringComparison.Ordinal))
            {
                throw new PricingCalculationException($"Snack line currency {line.UnitPrice.Currency} does not match {currency}.");
            }

            var net = Money.Multiply(line.UnitPrice, line.Quantity);
            var rate = _rates[line.VatType];
            var vatAmount = decimal.Round(net.Amount * rate, 2, MidpointRounding.AwayFromZero);
            sum += new Money(vatAmount, currency);
        }

        return sum;
    }
}
