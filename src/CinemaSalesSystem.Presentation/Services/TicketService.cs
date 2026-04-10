using CinemaSales.Application.Abstractions.Services;
using CinemaSales.ConsoleUI.Models;

namespace CinemaSales.ConsoleUI.Services;

public class TicketService
{
    private readonly ITicketService _ticketService;

    public TicketService(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    public async Task<TicketViewModel> PurchaseTicketAsync(Guid showTimeId, int quantity)
    {
        var result = await _ticketService.PurchaseTicketAsync(
            showTimeId,
            quantity,
            CancellationToken.None);

        return new TicketViewModel
        {
            ShowTimeId = result.ShowTimeId,
            Quantity = result.Quantity,
            UnitPrice = result.UnitPrice,
            TotalPrice = result.TotalPrice
        };
    }
}
