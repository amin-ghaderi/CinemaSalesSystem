using CinemaSales.Application.Abstractions.Services;
using CinemaSales.ConsoleUI.Models;

namespace CinemaSales.ConsoleUI.Services;

public class MovieService
{
    private readonly IMovieService _movieService;

    public MovieService(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public async Task<IEnumerable<MovieViewModel>> GetMoviesAsync()
    {
        var movies = await _movieService.GetAllMoviesAsync(CancellationToken.None);

        return movies.Select(m => new MovieViewModel
        {
            Id = m.Id,
            Title = m.Title,
            Genre = m.Genre,
            DurationInMinutes = m.DurationMinutes,
            Rating = m.Rating
        });
    }
}
