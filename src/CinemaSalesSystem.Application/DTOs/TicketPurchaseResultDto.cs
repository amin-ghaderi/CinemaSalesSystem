namespace CinemaSales.Application.DTOs;

public sealed class TicketPurchaseResultDto
{
    public Guid ShowTimeId { get; init; }

    public int Quantity { get; init; }

    public decimal UnitPrice { get; init; }

    public decimal TotalPrice { get; init; }
}
