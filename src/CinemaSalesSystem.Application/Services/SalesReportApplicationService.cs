using CinemaSales.Application.Abstractions.Persistence;
using CinemaSales.Application.Abstractions.Services;
using CinemaSales.Application.DTOs;
using CinemaSales.Domain.Aggregates.Orders;

namespace CinemaSales.Application.Services;

public class SalesReportApplicationService : ISalesReportService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IOrderSnackLineRepository _orderSnackLineRepository;

    public SalesReportApplicationService(
        ITicketRepository ticketRepository,
        IOrderSnackLineRepository orderSnackLineRepository)
    {
        _ticketRepository = ticketRepository;
        _orderSnackLineRepository = orderSnackLineRepository;
    }

    public async Task<SalesReportDto> GetSalesReportAsync(CancellationToken cancellationToken)
    {
        IReadOnlyList<Ticket> tickets = await _ticketRepository.GetAllAsync(cancellationToken);
        IReadOnlyList<OrderSnackLine> snackLines =
            await _orderSnackLineRepository.GetAllAsync(cancellationToken);

        decimal ticketRevenue = tickets.Sum(t => t.Price.Amount);
        int ticketsSold = tickets.Count;

        int snacksSold = snackLines.Sum(l => l.Quantity);
        decimal snackRevenue = snackLines.Sum(l => decimal.Round(
            l.UnitPrice.Amount * l.Quantity,
            2,
            MidpointRounding.AwayFromZero));

        decimal total = decimal.Round(ticketRevenue + snackRevenue, 2, MidpointRounding.AwayFromZero);

        return new SalesReportDto
        {
            TicketsSold = ticketsSold,
            SnacksSold = snacksSold,
            TotalTicketRevenue = ticketRevenue,
            TotalSnackRevenue = snackRevenue,
            TotalRevenue = total
        };
    }
}
