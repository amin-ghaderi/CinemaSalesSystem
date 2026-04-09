using CinemaSales.Domain.Entities;
using CinemaSales.Domain.Exceptions;
using CinemaSales.Domain.ValueObjects;

namespace CinemaSales.Domain.DomainServices;

/// <inheritdoc />
public sealed class DiscountService : IDiscountService
{
    /// <inheritdoc />
    public Money CalculateDiscountAmount(
        Money orderNetBeforeDiscount,
        Campaign campaign,
        DiscountCode code,
        DateTimeOffset asOf)
    {
        ArgumentNullException.ThrowIfNull(orderNetBeforeDiscount);
        ArgumentNullException.ThrowIfNull(campaign);
        ArgumentNullException.ThrowIfNull(code);

        campaign.EnsureActiveAt(asOf);

        if (!campaign.Matches(code))
        {
            throw new InvalidDiscountException("The discount code does not match this campaign.");
        }

        var pct = campaign.DiscountPercentage / 100m;
        var raw = decimal.Round(orderNetBeforeDiscount.Amount * pct, 2, MidpointRounding.AwayFromZero);
        var capped = Math.Min(raw, orderNetBeforeDiscount.Amount);
        return new Money(capped, orderNetBeforeDiscount.Currency);
    }
}
