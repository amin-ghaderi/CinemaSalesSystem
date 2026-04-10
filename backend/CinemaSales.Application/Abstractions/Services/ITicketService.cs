using CinemaSales.Application.DTOs;

namespace CinemaSales.Application.Abstractions.Services;

public interface ITicketService
{
    Task<TicketPurchaseResultDto> PurchaseTicketAsync(
        Guid showTimeId,
        int quantity,
        CancellationToken cancellationToken);

    Task<TicketDto> PurchaseTicketAsync(
        Guid showTimeId,
        string customerName,
        int seatNumber,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<TicketDto>> GetTicketsByShowTimeIdAsync(
        Guid showTimeId,
        CancellationToken cancellationToken);
}
