using CinemaSales.Domain.Common;

namespace CinemaSales.Domain.Events;

/// <summary>
/// Raised when a campaign discount is successfully applied to an order.
/// </summary>
/// <param name="OrderId">The order identifier.</param>
/// <param name="CampaignId">The campaign identifier.</param>
/// <param name="DiscountAmount">The monetary discount applied.</param>
/// <param name="Currency">The currency of the discount.</param>
public sealed record DiscountAppliedEvent(
    Guid OrderId,
    Guid CampaignId,
    decimal DiscountAmount,
    string Currency) : BaseDomainEvent;
