using CinemaSales.Domain.Aggregates.Orders;
using CinemaSales.Domain.ValueObjects;

namespace CinemaSales.Domain.DomainServices;

/// <summary>
/// Computes monetary totals for orders.
/// </summary>
public interface IPricingService
{
    /// <summary>
    /// Sums ticket prices, or zero in <paramref name="currency"/> when empty.
    /// </summary>
    Money CalculateTicketsSubtotal(IReadOnlyCollection<Ticket> tickets, string currency);

    /// <summary>
    /// Sums snack net amounts (unit × quantity), or zero in <paramref name="currency"/> when empty.
    /// </summary>
    Money CalculateSnacksNetSubtotal(IReadOnlyCollection<OrderSnackLine> lines, string currency);

    /// <summary>
    /// Computes final total: tickets + snack net + snack VAT − discount (discount capped at net before VAT).
    /// </summary>
    Money CalculateOrderTotal(
        Money ticketsSubtotal,
        Money snacksNetSubtotal,
        Money snackVatAmount,
        Money discountAmount);
}
