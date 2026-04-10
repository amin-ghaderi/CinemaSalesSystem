namespace CinemaSales.Application.Commands;

public sealed record PurchaseTicketCommand(
    Guid ShowTimeId,
    string CustomerName,
    int SeatNumber);
