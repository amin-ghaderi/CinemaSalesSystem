namespace CinemaSales.Application.DTOs;

public class SalesReportDto
{
    public decimal TotalTicketRevenue { get; set; }
    public decimal TotalSnackRevenue { get; set; }
    public decimal TotalRevenue { get; set; }
    public int TicketsSold { get; set; }
    public int SnacksSold { get; set; }
}
