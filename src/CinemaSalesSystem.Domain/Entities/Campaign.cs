using CinemaSales.Domain.Common;
using CinemaSales.Domain.Exceptions;
using CinemaSales.Domain.ValueObjects;

namespace CinemaSales.Domain.Entities;

/// <summary>
/// Promotional campaign that can reduce order totals when a matching code is supplied.
/// </summary>
public sealed class Campaign : Entity
{
    /// <summary>
    /// Initializes a new campaign.
    /// </summary>
    /// <param name="name">Campaign name.</param>
    /// <param name="discountPercentage">Percentage between 0 and 100.</param>
    /// <param name="discountCode">Required customer code.</param>
    /// <param name="validUntil">Last instant (UTC) the campaign is valid.</param>
    public Campaign(string name, decimal discountPercentage, string discountCode, DateTimeOffset validUntil)
        : base()
    {
        Name = Guard.AgainstNullOrWhiteSpace(name, nameof(name));
        DiscountPercentage = Guard.AgainstInvalidPercentage(discountPercentage, nameof(discountPercentage));
        DiscountCode = Guard.AgainstNullOrWhiteSpace(discountCode, nameof(discountCode)).Trim().ToUpperInvariant();
        ValidUntil = validUntil;
    }

    /// <summary>
    /// Initializes a campaign with a known identifier.
    /// </summary>
    public Campaign(Guid id, string name, decimal discountPercentage, string discountCode, DateTimeOffset validUntil)
        : base(id)
    {
        Name = Guard.AgainstNullOrWhiteSpace(name, nameof(name));
        DiscountPercentage = Guard.AgainstInvalidPercentage(discountPercentage, nameof(discountPercentage));
        DiscountCode = Guard.AgainstNullOrWhiteSpace(discountCode, nameof(discountCode)).Trim().ToUpperInvariant();
        ValidUntil = validUntil;
    }

    /// <summary>
    /// Gets the campaign name.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the discount percentage (0–100).
    /// </summary>
    public decimal DiscountPercentage { get; private set; }

    /// <summary>
    /// Gets the normalized discount code for comparison.
    /// </summary>
    public string DiscountCode { get; private set; }

    /// <summary>
    /// Gets the last valid instant for the campaign.
    /// </summary>
    public DateTimeOffset ValidUntil { get; private set; }

    /// <summary>
    /// Returns whether the campaign is active at the given instant.
    /// </summary>
    /// <param name="asOf">The instant to evaluate.</param>
    public bool IsActiveAt(DateTimeOffset asOf) => asOf <= ValidUntil;

    /// <summary>
    /// Ensures the campaign is active at the given instant.
    /// </summary>
    /// <param name="asOf">The instant to evaluate.</param>
    /// <exception cref="ExpiredCampaignException">When the campaign has expired.</exception>
    public void EnsureActiveAt(DateTimeOffset asOf)
    {
        if (!IsActiveAt(asOf))
        {
            throw new ExpiredCampaignException($"Campaign '{Name}' is not valid after {ValidUntil:O}.");
        }
    }

    /// <summary>
    /// Returns whether the supplied code matches this campaign.
    /// </summary>
    /// <param name="code">The customer code.</param>
    public bool Matches(DiscountCode code)
    {
        ArgumentNullException.ThrowIfNull(code);
        return string.Equals(DiscountCode, code.Code, StringComparison.Ordinal);
    }
}
