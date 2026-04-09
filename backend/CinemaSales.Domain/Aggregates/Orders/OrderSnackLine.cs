using CinemaSales.Domain.Common;
using CinemaSales.Domain.Enums;
using CinemaSales.Domain.ValueObjects;

namespace CinemaSales.Domain.Aggregates.Orders;

/// <summary>
/// Concession line on an <see cref="Order"/>.
/// </summary>
public sealed class OrderSnackLine : Entity
{
    /// <summary>
    /// Initializes a snack line.
    /// </summary>
    /// <param name="snackId">Catalog snack identifier.</param>
    /// <param name="quantity">Quantity purchased.</param>
    /// <param name="unitPrice">Net unit price.</param>
    /// <param name="vatType">VAT classification for this line.</param>
    public OrderSnackLine(Guid snackId, int quantity, Money unitPrice, VatType vatType)
        : base()
    {
        SnackId = Guard.AgainstEmpty(snackId, nameof(snackId));
        Quantity = Guard.AgainstNonPositive(quantity, nameof(quantity));
        UnitPrice = Guard.AgainstNull(unitPrice, nameof(unitPrice));
        VatType = vatType;
    }

    /// <summary>
    /// Initializes a snack line with a known identifier.
    /// </summary>
    public OrderSnackLine(Guid id, Guid snackId, int quantity, Money unitPrice, VatType vatType)
        : base(id)
    {
        SnackId = Guard.AgainstEmpty(snackId, nameof(snackId));
        Quantity = Guard.AgainstNonPositive(quantity, nameof(quantity));
        UnitPrice = Guard.AgainstNull(unitPrice, nameof(unitPrice));
        VatType = vatType;
    }

    /// <summary>
    /// Gets the snack catalog identifier.
    /// </summary>
    public Guid SnackId { get; private set; }

    /// <summary>
    /// Gets the quantity.
    /// </summary>
    public int Quantity { get; private set; }

    /// <summary>
    /// Gets the net unit price.
    /// </summary>
    public Money UnitPrice { get; private set; }

    /// <summary>
    /// Gets the VAT classification.
    /// </summary>
    public VatType VatType { get; private set; }
}
