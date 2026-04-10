using CinemaSales.Application.DTOs;

namespace CinemaSales.Application.Abstractions.Services;

public interface ISalesReportService
{
    Task<SalesReportDto> GetSalesReportAsync(CancellationToken cancellationToken);
}
