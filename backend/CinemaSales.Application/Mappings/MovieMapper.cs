using CinemaSales.Application.DTOs;
using CinemaSales.Domain.Aggregates.Movies;

namespace CinemaSales.Application.Mappings;

public static class MovieMapper
{
    public static MovieDto ToDto(Movie movie)
    {
        return new MovieDto
        {
            Id = movie.Id,
            Title = movie.Title,
            DurationMinutes = movie.DurationMinutes,
            Rating = movie.Rating.ToString()
        };
    }
}
