using CinemaSales.Application.DTOs;
using CinemaSales.Domain.Aggregates.Movies;

namespace CinemaSales.Application.Mappings;

public static class ShowTimeMapper
{
    public static ShowTimeDto ToDto(
        ShowTime showTime,
        string movieTitle = "",
        decimal listPrice = 0)
    {
        ArgumentNullException.ThrowIfNull(showTime);

        return new ShowTimeDto
        {
            Id = showTime.Id,
            MovieId = showTime.MovieId,
            MovieTitle = movieTitle,
            StartTime = showTime.StartTime.UtcDateTime,
            Auditorium = showTime.Auditorium,
            Slot = showTime.Slot.ToString(),
            Price = listPrice
        };
    }
}
