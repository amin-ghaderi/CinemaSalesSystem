using CinemaSales.Application.DTOs;
using CinemaSales.Domain.Entities;

namespace CinemaSales.Application.Mappings;

public static class SnackMapper
{
    public static SnackDto ToDto(Snack snack)
    {
        return new SnackDto
        {
            Id = snack.Id,
            Name = snack.Name,
            Price = snack.Price.Amount
        };
    }
}
