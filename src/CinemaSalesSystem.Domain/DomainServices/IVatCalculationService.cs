using CinemaSales.Domain.Aggregates.Orders;
using CinemaSales.Domain.ValueObjects;

namespace CinemaSales.Domain.DomainServices;

/// <summary>
/// Calculates VAT amounts for order lines.
/// </summary>
public interface IVatCalculationService
{
    /// <summary>
    /// Calculates VAT for all snack lines using configured rates per <see cref="Enums.VatType"/>.
    /// Returns <see cref="Money.Zero"/> in <paramref name="currency"/> when there are no lines.
    /// </summary>
    Money CalculateVatForSnackLines(IReadOnlyCollection<OrderSnackLine> lines, string currency);
}
