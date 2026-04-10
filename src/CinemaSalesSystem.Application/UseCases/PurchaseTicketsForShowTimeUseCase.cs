using CinemaSales.Application.Abstractions.Persistence;
using CinemaSales.Application.DTOs;
using CinemaSales.Application.Pricing;
using CinemaSales.Domain.Aggregates.Movies;
using CinemaSales.Domain.Aggregates.Orders;
using CinemaSales.Domain.Enums;
using CinemaSales.Domain.ValueObjects;

namespace CinemaSales.Application.UseCases;

/// <summary>
/// Purchases multiple regular tickets for a show time, assigning the next available seats (workshop layout: row A, seats 1–54).
/// </summary>
public sealed class PurchaseTicketsForShowTimeUseCase
{
    public const int MaxSeatsPerShowTime = 54;

    private readonly IShowTimeRepository _showTimeRepository;
    private readonly ITicketRepository _ticketRepository;

    public PurchaseTicketsForShowTimeUseCase(
        IShowTimeRepository showTimeRepository,
        ITicketRepository ticketRepository)
    {
        _showTimeRepository = showTimeRepository;
        _ticketRepository = ticketRepository;
    }

    public async Task<TicketPurchaseResultDto> ExecuteAsync(
        Guid showTimeId,
        int quantity,
        CancellationToken cancellationToken)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be positive.");
        }

        ShowTime? showTime = await _showTimeRepository.GetByIdAsync(showTimeId, cancellationToken) ?? throw new InvalidOperationException("Show time was not found.");
        decimal unitPrice = ShowTimeListPricing.GetListPrice(showTime.Slot);
        var unitMoney = new Money(unitPrice, "SEK");

        IReadOnlyList<Ticket> existing = await _ticketRepository.GetByShowTimeIdAsync(
            showTimeId,
            cancellationToken);

        var taken = existing
            .Where(t => string.Equals(t.SeatNumber.Row, "A", StringComparison.Ordinal))
            .Select(t => t.SeatNumber.Number)
            .ToHashSet();

        var assignedSeats = new List<int>(quantity);
        for (int n = 1; n <= MaxSeatsPerShowTime && assignedSeats.Count < quantity; n++)
        {
            if (!taken.Contains(n))
            {
                assignedSeats.Add(n);
            }
        }

        if (assignedSeats.Count < quantity)
        {
            throw new InvalidOperationException(
                $"Not enough seats available (requested {quantity}, assigned {assignedSeats.Count}).");
        }

        Guid orderId = Guid.NewGuid();
        const string customerRow = "A";

        foreach (int seatNum in assignedSeats)
        {
            var seat = new SeatNumber(customerRow, seatNum);
            var ticket = new Ticket(orderId, showTimeId, seat, TicketType.Regular, unitMoney);
            await _ticketRepository.AddAsync(ticket, cancellationToken);
        }

        decimal total = unitPrice * quantity;

        return new TicketPurchaseResultDto
        {
            ShowTimeId = showTimeId,
            Quantity = quantity,
            UnitPrice = unitPrice,
            TotalPrice = total
        };
    }
}
