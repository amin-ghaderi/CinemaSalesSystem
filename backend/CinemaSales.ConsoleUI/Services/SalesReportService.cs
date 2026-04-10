using CinemaSales.Application.Abstractions.Services;
using CinemaSales.ConsoleUI.Models;

namespace CinemaSales.ConsoleUI.Services;

public class SalesReportService
{
    private readonly ISalesReportService _salesReportService;

    public SalesReportService(ISalesReportService salesReportService)
    {
        _salesReportService = salesReportService;
    }

    public async Task<SalesReportViewModel> GetSalesReportAsync()
    {
        var report = await _salesReportService.GetSalesReportAsync(CancellationToken.None);

        return new SalesReportViewModel
        {
            TotalTicketRevenue = report.TotalTicketRevenue,
            TotalSnackRevenue = report.TotalSnackRevenue,
            TotalRevenue = report.TotalRevenue,
            TicketsSold = report.TicketsSold,
            SnacksSold = report.SnacksSold
        };
    }
}
