using CinemaSales.Domain.Aggregates.Orders;
using CinemaSales.Domain.Common;
using CinemaSales.Domain.Exceptions;
using CinemaSales.Domain.ValueObjects;

namespace CinemaSales.Domain.DomainServices;

/// <inheritdoc cref="IPricingService" />
public sealed class PricingService : IPricingService
{
    /// <inheritdoc />
    public Money CalculateTicketsSubtotal(IReadOnlyCollection<Ticket> tickets, string currency)
    {
        Guard.AgainstNullOrWhiteSpace(currency, nameof(currency));
        ArgumentNullException.ThrowIfNull(tickets);

        if (tickets.Count == 0)
        {
            return Money.Zero(currency);
        }

        Money sum = Money.Zero(currency);
        foreach (var ticket in tickets)
        {
            EnsureCurrency(ticket.Price, currency);
            sum += ticket.Price;
        }

        return sum;
    }

    /// <inheritdoc />
    public Money CalculateSnacksNetSubtotal(IReadOnlyCollection<OrderSnackLine> lines, string currency)
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
            EnsureCurrency(line.UnitPrice, currency);
            var lineTotal = Money.Multiply(line.UnitPrice, line.Quantity);
            sum += lineTotal;
        }

        return sum;
    }

    /// <inheritdoc />
    public Money CalculateOrderTotal(
        Money ticketsSubtotal,
        Money snacksNetSubtotal,
        Money snackVatAmount,
        Money discountAmount)
    {
        ArgumentNullException.ThrowIfNull(ticketsSubtotal);
        ArgumentNullException.ThrowIfNull(snacksNetSubtotal);
        ArgumentNullException.ThrowIfNull(snackVatAmount);
        ArgumentNullException.ThrowIfNull(discountAmount);

        EnsureSameCurrency(ticketsSubtotal, snacksNetSubtotal);
        EnsureSameCurrency(ticketsSubtotal, snackVatAmount);
        EnsureSameCurrency(ticketsSubtotal, discountAmount);

        var gross = ticketsSubtotal + snacksNetSubtotal + snackVatAmount;
        if (discountAmount.Amount > gross.Amount)
        {
            throw new InvalidDiscountException("Discount cannot exceed the order total (including VAT).");
        }

        return new Money(gross.Amount - discountAmount.Amount, gross.Currency);
    }

    private static void EnsureCurrency(Money money, string expectedCurrency)
    {
        if (!string.Equals(money.Currency, expectedCurrency, StringComparison.Ordinal))
        {
            throw new PricingCalculationException($"Expected currency {expectedCurrency} but found {money.Currency}.");
        }
    }

    private static void EnsureSameCurrency(Money left, Money right)
    {
        if (!string.Equals(left.Currency, right.Currency, StringComparison.Ordinal))
        {
            throw new PricingCalculationException($"Currency mismatch: {left.Currency} vs {right.Currency}.");
        }
    }
}
