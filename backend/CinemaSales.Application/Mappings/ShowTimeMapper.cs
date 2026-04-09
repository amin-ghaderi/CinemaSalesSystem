using CinemaSales.Application.DTOs;
using CinemaSales.Domain.Aggregates.Movies;

namespace CinemaSales.Application.Mappings;

public static class ShowTimeMapper
{
    public static ShowTimeDto ToDto(ShowTime showTime)
    {
        return new ShowTimeDto
        {
            Id = showTime.Id,
            MovieId = showTime.MovieId,
            StartTime = showTime.StartTime.UtcDateTime,
            Auditorium = showTime.Auditorium,
            Slot = showTime.Slot.ToString()
        };
    }
}
