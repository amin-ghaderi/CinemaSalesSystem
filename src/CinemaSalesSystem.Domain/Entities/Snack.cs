using CinemaSales.Domain.Common;
using CinemaSales.Domain.Enums;
using CinemaSales.Domain.ValueObjects;

namespace CinemaSales.Domain.Entities;

/// <summary>
/// Concession item available for sale.
/// </summary>
public sealed class Snack : Entity
{
    /// <summary>
    /// Initializes a new snack.
    /// </summary>
    /// <param name="name">Display name.</param>
    /// <param name="price">Unit price (net, before VAT).</param>
    /// <param name="vatType">VAT classification for the item.</param>
    public Snack(string name, Money price, VatType vatType)
        : base()
    {
        Name = Guard.AgainstNullOrWhiteSpace(name, nameof(name));
        Price = Guard.AgainstNull(price, nameof(price));
        VatType = vatType;
    }

    /// <summary>
    /// Initializes a snack with a known identifier (e.g. for rehydration).
    /// </summary>
    public Snack(Guid id, string name, Money price, VatType vatType)
        : base(id)
    {
        Name = Guard.AgainstNullOrWhiteSpace(name, nameof(name));
        Price = Guard.AgainstNull(price, nameof(price));
        VatType = vatType;
    }

    /// <summary>
    /// Gets the snack name.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the unit price (net).
    /// </summary>
    public Money Price { get; private set; }

    /// <summary>
    /// Gets the VAT classification.
    /// </summary>
    public VatType VatType { get; private set; }
}
