using CinemaSales.Domain.Common;
using CinemaSales.Domain.DomainServices;
using CinemaSales.Domain.Entities;
using CinemaSales.Domain.Enums;
using CinemaSales.Domain.Events;
using CinemaSales.Domain.Exceptions;
using CinemaSales.Domain.ValueObjects;

namespace CinemaSales.Domain.Aggregates.Orders;

/// <summary>
/// Aggregate root for a customer purchase containing tickets and optional snacks.
/// </summary>
public sealed class Order : AggregateRoot
{
    private const string DefaultCurrency = "EUR";

    private readonly List<Ticket> _tickets = new();
    private readonly List<OrderSnackLine> _snackLines = new();
    private Guid? _appliedCampaignId;
    private Money? _appliedDiscount;

    private Order()
    {
        CreatedAt = DateTimeOffset.UtcNow;
        Status = OrderStatus.Pending;
        TotalAmount = Money.Zero(DefaultCurrency);
    }

    /// <summary>
    /// Gets the creation timestamp (UTC).
    /// </summary>
    public DateTimeOffset CreatedAt { get; private set; }

    /// <summary>
    /// Gets the order lifecycle status.
    /// </summary>
    public OrderStatus Status { get; private set; }

    /// <summary>
    /// Gets the last calculated total payable amount.
    /// </summary>
    public Money TotalAmount { get; private set; }

    /// <summary>
    /// Gets the applied campaign identifier, if any.
    /// </summary>
    public Guid? AppliedCampaignId => _appliedCampaignId;

    /// <summary>
    /// Gets the discount applied to the order net amount, if any.
    /// </summary>
    public Money? AppliedDiscount => _appliedDiscount;

    /// <summary>
    /// Gets ticket lines on this order.
    /// </summary>
    public IReadOnlyCollection<Ticket> Tickets => _tickets.AsReadOnly();

    /// <summary>
    /// Gets snack lines on this order.
    /// </summary>
    public IReadOnlyCollection<OrderSnackLine> SnackLines => _snackLines.AsReadOnly();

    /// <summary>
    /// Creates a new empty pending order.
    /// </summary>
    public static Order Create()
    {
        var order = new Order();
        order.RaiseDomainEvent(new OrderCreatedEvent(order.Id));
        return order;
    }

    /// <summary>
    /// Resolves the billing currency from existing lines.
    /// </summary>
    /// <exception cref="PricingCalculationException">When the order has no lines.</exception>
    public string ResolveBillingCurrency() => RequireCurrency();

    /// <summary>
    /// Adds a ticket line after validating seat availability for the show time.
    /// </summary>
    /// <remarks>
    /// The caller must ensure <paramref name="showTimeId"/> refers to an existing, bookable show time;
    /// the domain layer does not resolve movies or show times from persistence.
    /// </remarks>
    /// <param name="showTimeId">Valid show time identifier.</param>
    /// <param name="seatNumber">Seat to reserve.</param>
    /// <param name="ticketType">Ticket category.</param>
    /// <param name="price">Ticket price.</param>
    /// <param name="seatAllocation">Domain service for reservation rules.</param>
    /// <param name="existingTicketsForShowTime">All tickets already sold for this screening (any order).</param>
    /// <param name="pricingService">Pricing domain service.</param>
    /// <param name="vatService">VAT domain service.</param>
    public void AddTicket(
        Guid showTimeId,
        SeatNumber seatNumber,
        TicketType ticketType,
        Money price,
        ISeatAllocationService seatAllocation,
        IEnumerable<Ticket> existingTicketsForShowTime,
        IPricingService pricingService,
        IVatCalculationService vatService)
    {
        ArgumentNullException.ThrowIfNull(seatAllocation);
        ArgumentNullException.ThrowIfNull(existingTicketsForShowTime);
        ArgumentNullException.ThrowIfNull(pricingService);
        ArgumentNullException.ThrowIfNull(vatService);
        ArgumentNullException.ThrowIfNull(price);
        ArgumentNullException.ThrowIfNull(seatNumber);
        Guard.AgainstEmpty(showTimeId, nameof(showTimeId));

        EnsurePending();

        seatAllocation.EnsureSeatAvailableForShowTime(showTimeId, seatNumber, existingTicketsForShowTime);

        var ticket = new Ticket(Id, showTimeId, seatNumber, ticketType, price);
        _tickets.Add(ticket);

        RaiseDomainEvent(new TicketReservedEvent(Id, showTimeId, seatNumber.Row, seatNumber.Number));
        RaiseDomainEvent(new SeatReservedEvent(showTimeId, seatNumber.Row, seatNumber.Number));

        RecalculateTotals(pricingService, vatService);
    }

    /// <summary>
    /// Adds a snack line to the order.
    /// </summary>
    public void AddSnackLine(
        Guid snackId,
        int quantity,
        Money unitPrice,
        VatType vatType,
        IPricingService pricingService,
        IVatCalculationService vatService)
    {
        ArgumentNullException.ThrowIfNull(pricingService);
        ArgumentNullException.ThrowIfNull(vatService);
        ArgumentNullException.ThrowIfNull(unitPrice);
        Guard.AgainstEmpty(snackId, nameof(snackId));

        EnsurePending();

        var line = new OrderSnackLine(snackId, quantity, unitPrice, vatType);
        _snackLines.Add(line);

        RecalculateTotals(pricingService, vatService);
    }

    /// <summary>
    /// Applies a campaign discount to the current net goods total (tickets + snack net).
    /// </summary>
    public void ApplyCampaign(
        Campaign campaign,
        DiscountCode code,
        DateTimeOffset asOf,
        IDiscountService discountService,
        IPricingService pricingService,
        IVatCalculationService vatService)
    {
        ArgumentNullException.ThrowIfNull(campaign);
        ArgumentNullException.ThrowIfNull(code);
        ArgumentNullException.ThrowIfNull(discountService);
        ArgumentNullException.ThrowIfNull(pricingService);
        ArgumentNullException.ThrowIfNull(vatService);

        EnsurePending();

        var currency = RequireCurrency();
        var ticketSub = pricingService.CalculateTicketsSubtotal(_tickets, currency);
        var snackNet = pricingService.CalculateSnacksNetSubtotal(_snackLines, currency);
        var netBeforeDiscount = ticketSub + snackNet;

        var discount = discountService.CalculateDiscountAmount(netBeforeDiscount, campaign, code, asOf);
        _appliedCampaignId = campaign.Id;
        _appliedDiscount = discount;

        RaiseDomainEvent(new DiscountAppliedEvent(Id, campaign.Id, discount.Amount, discount.Currency));

        RecalculateTotals(pricingService, vatService);
    }

    /// <summary>
    /// Marks the order as paid after validating invariants.
    /// </summary>
    /// <exception cref="PricingCalculationException">When the order has no lines.</exception>
    public void MarkAsPaid()
    {
        EnsurePending();

        if (_tickets.Count == 0 && _snackLines.Count == 0)
        {
            throw new PricingCalculationException(
                "Orders must contain at least one ticket or snack line before payment.");
        }

        Status = OrderStatus.Paid;
    }

    /// <summary>
    /// Cancels a pending order.
    /// </summary>
    public void Cancel()
    {
        EnsurePending();
        Status = OrderStatus.Cancelled;
    }

    private void EnsurePending()
    {
        if (Status != OrderStatus.Pending)
        {
            throw new InvalidOperationException("The operation is only valid for pending orders.");
        }
    }

    private string RequireCurrency()
    {
        var ticket = _tickets.FirstOrDefault();
        if (ticket is not null)
        {
            return ticket.Price.Currency;
        }

        var snack = _snackLines.FirstOrDefault();
        if (snack is not null)
        {
            return snack.UnitPrice.Currency;
        }

        throw new PricingCalculationException("The order has no lines; currency cannot be determined.");
    }

    private void RecalculateTotals(IPricingService pricingService, IVatCalculationService vatService)
    {
        if (_tickets.Count == 0 && _snackLines.Count == 0)
        {
            TotalAmount = Money.Zero(DefaultCurrency);
            return;
        }

        var currency = RequireCurrency();
        var ticketSub = pricingService.CalculateTicketsSubtotal(_tickets, currency);
        var snackNet = pricingService.CalculateSnacksNetSubtotal(_snackLines, currency);
        var snackVat = vatService.CalculateVatForSnackLines(_snackLines, currency);
        var discount = _appliedDiscount ?? Money.Zero(currency);
        TotalAmount = pricingService.CalculateOrderTotal(ticketSub, snackNet, snackVat, discount);
    }
}
