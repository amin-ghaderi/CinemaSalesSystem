using CinemaSales.Domain.Entities;
using CinemaSales.Domain.ValueObjects;

namespace CinemaSales.Domain.DomainServices;

/// <summary>
/// Applies campaign discounts to order net amounts.
/// </summary>
public interface IDiscountService
{
    /// <summary>
    /// Validates the campaign and code, then returns the discount amount (never exceeding the net subtotal).
    /// </summary>
    Money CalculateDiscountAmount(
        Money orderNetBeforeDiscount,
        Campaign campaign,
        DiscountCode code,
        DateTimeOffset asOf);
}
