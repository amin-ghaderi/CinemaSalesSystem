using CinemaSales.Application.Abstractions.Persistence;
using CinemaSales.Application.Commands;
using CinemaSales.Application.DTOs;
using CinemaSales.Application.Mappings;
using CinemaSales.Domain.Aggregates.Orders;
using CinemaSales.Domain.Enums;
using CinemaSales.Domain.ValueObjects;

namespace CinemaSales.Application.UseCases;

/// <summary>
/// Persists a ticket line. Full order workflow, pricing, and seat rules belong in the domain/application evolution;
/// this use case builds a persistable <see cref="Ticket"/> with explicit placeholders where the model requires them.
/// </summary>
public sealed class PurchaseTicketUseCase
{
    private readonly ITicketRepository _ticketRepository;

    public PurchaseTicketUseCase(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<TicketDto> ExecuteAsync(
        PurchaseTicketCommand command,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        // Placeholder until an Order aggregate and pricing pipeline own ticket creation.
        Guid provisionalOrderId = Guid.NewGuid();
        var seat = new SeatNumber("A", command.SeatNumber);
        var price = new Money(0m, "EUR");

        var ticket = new Ticket(
            provisionalOrderId,
            command.ShowTimeId,
            seat,
            TicketType.Regular,
            price);

        await _ticketRepository.AddAsync(ticket, cancellationToken);

        return TicketMapper.ToDto(ticket, command.CustomerName);
    }
}
