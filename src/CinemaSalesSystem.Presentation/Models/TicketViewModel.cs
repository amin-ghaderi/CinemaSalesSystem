namespace CinemaSales.ConsoleUI.Models;

public class TicketViewModel
{
    public Guid ShowTimeId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalPrice { get; set; }
}
