namespace CinemaSales.Application.DTOs;

public sealed class SnackPurchaseResultDto
{
    public string SnackName { get; init; } = string.Empty;

    public int Quantity { get; init; }

    public decimal UnitPrice { get; init; }

    public decimal TotalPrice { get; init; }
}
