using CinemaSales.Application.DTOs;
using CinemaSales.Domain.Aggregates.Orders;

namespace CinemaSales.Application.Mappings;

public static class TicketMapper
{
    public static TicketDto ToDto(Ticket ticket, string customerName)
    {
        return new TicketDto
        {
            Id = ticket.Id,
            ShowTimeId = ticket.ShowTimeId,
            CustomerName = customerName,
            SeatNumber = ticket.SeatNumber.Number
        };
    }
}
