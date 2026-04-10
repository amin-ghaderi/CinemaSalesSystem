using CinemaSales.Application.Abstractions.Services;
using CinemaSales.ConsoleUI.Models;

namespace CinemaSales.ConsoleUI.Services;

public class ShowTimeService
{
    private readonly IShowTimeService _showTimeService;

    public ShowTimeService(IShowTimeService showTimeService)
    {
        _showTimeService = showTimeService;
    }

    public async Task<IEnumerable<ShowTimeViewModel>> GetShowTimesAsync()
    {
        var showTimes = await _showTimeService.GetAllShowTimesAsync(CancellationToken.None);

        return showTimes.Select(st => new ShowTimeViewModel
        {
            Id = st.Id,
            MovieTitle = st.MovieTitle,
            StartTime = st.StartTime,
            Auditorium = st.Auditorium,
            Price = st.Price
        });
    }
}
