namespace CinemaSales.Application.DTOs;

public sealed class TicketDto
{
    public Guid Id { get; init; }
    public Guid ShowTimeId { get; init; }
    public string CustomerName { get; init; } = string.Empty;
    public int SeatNumber { get; init; }
}
