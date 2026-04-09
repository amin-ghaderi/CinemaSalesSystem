using CinemaSales.Domain.Common;

namespace CinemaSales.Domain.ValueObjects;

/// <summary>
/// Describes an audience age band used for ticket pricing rules.
/// </summary>
public sealed record AgeCategory
{
    /// <summary>
    /// Initializes a new instance of <see cref="AgeCategory"/>.
    /// </summary>
    /// <param name="categoryName">Human-readable category name.</param>
    /// <param name="minimumAge">Minimum age for the category.</param>
    public AgeCategory(string categoryName, int minimumAge)
    {
        CategoryName = Guard.AgainstNullOrWhiteSpace(categoryName, nameof(categoryName));
        MinimumAge = Guard.AgainstNegative(minimumAge, nameof(minimumAge));
    }

    /// <summary>
    /// Gets the category name.
    /// </summary>
    public string CategoryName { get; }

    /// <summary>
    /// Gets the minimum age for this category.
    /// </summary>
    public int MinimumAge { get; }
}
