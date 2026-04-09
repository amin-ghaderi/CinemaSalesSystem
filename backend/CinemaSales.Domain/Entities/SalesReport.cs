using CinemaSales.Domain.Aggregates.Orders;
using CinemaSales.Domain.Common;
using CinemaSales.Domain.DomainServices;
using CinemaSales.Domain.Enums;
using CinemaSales.Domain.Exceptions;
using CinemaSales.Domain.ValueObjects;

namespace CinemaSales.Domain.Entities;

/// <summary>
/// Aggregated financial snapshot for a single reporting day.
/// </summary>
public sealed class SalesReport : Entity
{
    private SalesReport(
        Guid id,
        DateOnly reportDate,
        Money totalRevenue,
        Money totalVat,
        int totalTicketsSold)
        : base(id)
    {
        ReportDate = reportDate;
        TotalRevenue = Guard.AgainstNull(totalRevenue, nameof(totalRevenue));
        TotalVat = Guard.AgainstNull(totalVat, nameof(totalVat));
        TotalTicketsSold = Guard.AgainstNegative(totalTicketsSold, nameof(totalTicketsSold));
    }

    /// <summary>
    /// Gets the calendar date of the report.
    /// </summary>
    public DateOnly ReportDate { get; }

    /// <summary>
    /// Gets total revenue from paid orders on the report date.
    /// </summary>
    public Money TotalRevenue { get; }

    /// <summary>
    /// Gets total VAT on snack lines for included orders.
    /// </summary>
    public Money TotalVat { get; }

    /// <summary>
    /// Gets the number of tickets sold across included orders.
    /// </summary>
    public int TotalTicketsSold { get; }

    /// <summary>
    /// Builds a report from paid orders whose creation date (UTC) falls on <paramref name="reportDate"/>.
    /// Only orders billed in <paramref name="currency"/> are included.
    /// </summary>
    /// <param name="reportDate">Reporting day.</param>
    /// <param name="orders">Candidate orders.</param>
    /// <param name="currency">Reporting currency filter.</param>
    /// <param name="vatService">VAT calculator for snack lines.</param>
    public static SalesReport FromPaidOrders(
        DateOnly reportDate,
        IEnumerable<Order> orders,
        string currency,
        IVatCalculationService vatService)
    {
        ArgumentNullException.ThrowIfNull(orders);
        Guard.AgainstNullOrWhiteSpace(currency, nameof(currency));
        ArgumentNullException.ThrowIfNull(vatService);

        Money revenue = Money.Zero(currency);
        Money vat = Money.Zero(currency);
        var tickets = 0;

        foreach (var order in orders)
        {
            ArgumentNullException.ThrowIfNull(order);

            if (order.Status != OrderStatus.Paid)
            {
                continue;
            }

            if (DateOnly.FromDateTime(order.CreatedAt.UtcDateTime) != reportDate)
            {
                continue;
            }

            string billCurrency;
            try
            {
                billCurrency = order.ResolveBillingCurrency();
            }
            catch (PricingCalculationException)
            {
                continue;
            }

            if (!string.Equals(billCurrency, currency, StringComparison.Ordinal))
            {
                continue;
            }

            revenue += order.TotalAmount;
            vat += vatService.CalculateVatForSnackLines(order.SnackLines, currency);
            tickets += order.Tickets.Count;
        }

        return new SalesReport(Guid.NewGuid(), reportDate, revenue, vat, tickets);
    }
}
