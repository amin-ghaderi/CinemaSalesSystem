using CinemaSales.Domain.Common;
using CinemaSales.Domain.Enums;
using CinemaSales.Domain.ValueObjects;

namespace CinemaSales.Domain.Aggregates.Orders;

/// <summary>
/// Ticket line belonging to an <see cref="Order"/> aggregate.
/// </summary>
public sealed class Ticket : Entity
{
    /// <summary>
    /// Initializes a ticket line for an order.
    /// </summary>
    /// <param name="orderId">Owning order.</param>
    /// <param name="showTimeId">Target show time.</param>
    /// <param name="seatNumber">Reserved seat.</param>
    /// <param name="ticketType">Pricing category.</param>
    /// <param name="price">Ticket price.</param>
    public Ticket(Guid orderId, Guid showTimeId, SeatNumber seatNumber, TicketType ticketType, Money price)
        : base()
    {
        OrderId = Guard.AgainstEmpty(orderId, nameof(orderId));
        ShowTimeId = Guard.AgainstEmpty(showTimeId, nameof(showTimeId));
        SeatNumber = Guard.AgainstNull(seatNumber, nameof(seatNumber));
        TicketType = ticketType;
        Price = Guard.AgainstNull(price, nameof(price));
    }

    /// <summary>
    /// Initializes a ticket with a known identifier.
    /// </summary>
    public Ticket(Guid id, Guid orderId, Guid showTimeId, SeatNumber seatNumber, TicketType ticketType, Money price)
        : base(id)
    {
        OrderId = Guard.AgainstEmpty(orderId, nameof(orderId));
        ShowTimeId = Guard.AgainstEmpty(showTimeId, nameof(showTimeId));
        SeatNumber = Guard.AgainstNull(seatNumber, nameof(seatNumber));
        TicketType = ticketType;
        Price = Guard.AgainstNull(price, nameof(price));
    }

    /// <summary>
    /// Gets the owning order identifier.
    /// </summary>
    public Guid OrderId { get; private set; }

    /// <summary>
    /// Gets the show time identifier.
    /// </summary>
    public Guid ShowTimeId { get; private set; }

    /// <summary>
    /// Gets the seat number for this ticket.
    /// </summary>
    public SeatNumber SeatNumber { get; private set; }

    /// <summary>
    /// Gets the ticket type.
    /// </summary>
    public TicketType TicketType { get; private set; }

    /// <summary>
    /// Gets the ticket price.
    /// </summary>
    public Money Price { get; private set; }
}
