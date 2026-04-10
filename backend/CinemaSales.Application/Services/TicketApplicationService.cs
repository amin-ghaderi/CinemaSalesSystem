using CinemaSales.Application.Abstractions.Persistence;
using CinemaSales.Application.Abstractions.Services;
using CinemaSales.Application.Commands;
using CinemaSales.Application.DTOs;
using CinemaSales.Application.Mappings;
using CinemaSales.Application.UseCases;

namespace CinemaSales.Application.Services;

public sealed class TicketApplicationService : ITicketService
{
    private readonly PurchaseTicketUseCase _purchaseSingle;
    private readonly PurchaseTicketsForShowTimeUseCase _purchaseBatch;
    private readonly ITicketRepository _ticketRepository;

    public TicketApplicationService(
        PurchaseTicketUseCase purchaseSingle,
        PurchaseTicketsForShowTimeUseCase purchaseBatch,
        ITicketRepository ticketRepository)
    {
        _purchaseSingle = purchaseSingle;
        _purchaseBatch = purchaseBatch;
        _ticketRepository = ticketRepository;
    }

    public Task<TicketPurchaseResultDto> PurchaseTicketAsync(
        Guid showTimeId,
        int quantity,
        CancellationToken cancellationToken) =>
        _purchaseBatch.ExecuteAsync(showTimeId, quantity, cancellationToken);

    public async Task<TicketDto> PurchaseTicketAsync(
        Guid showTimeId,
        string customerName,
        int seatNumber,
        CancellationToken cancellationToken)
    {
        var command = new PurchaseTicketCommand(showTimeId, customerName, seatNumber);
        return await _purchaseSingle.ExecuteAsync(command, cancellationToken);
    }

    public async Task<IReadOnlyList<TicketDto>> GetTicketsByShowTimeIdAsync(
        Guid showTimeId,
        CancellationToken cancellationToken)
    {
        var list = await _ticketRepository.GetByShowTimeIdAsync(showTimeId, cancellationToken);
        return list.Select(t => TicketMapper.ToDto(t, string.Empty)).ToList();
    }
}
