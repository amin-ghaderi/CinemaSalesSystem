using CinemaSales.Domain.Enums;

namespace CinemaSales.Application.Pricing;

/// <summary>
/// Workshop list prices (regular adult tier, SEK) referenced in seed documentation.
/// </summary>
public static class ShowTimeListPricing
{
    public static decimal GetListPrice(ShowTimeSlot slot) =>
        slot == ShowTimeSlot.Afternoon ? 105m : 130m;
}
