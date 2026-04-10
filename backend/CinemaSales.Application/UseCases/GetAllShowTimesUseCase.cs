using CinemaSales.Application.Abstractions.Persistence;
using CinemaSales.Application.DTOs;
using CinemaSales.Application.Mappings;
using CinemaSales.Application.Pricing;
using CinemaSales.Domain.Aggregates.Movies;

namespace CinemaSales.Application.UseCases;

public sealed class GetAllShowTimesUseCase
{
    private readonly IShowTimeRepository _showTimeRepository;
    private readonly IMovieRepository _movieRepository;

    public GetAllShowTimesUseCase(
        IShowTimeRepository showTimeRepository,
        IMovieRepository movieRepository)
    {
        _showTimeRepository = showTimeRepository;
        _movieRepository = movieRepository;
    }

    public async Task<IReadOnlyList<ShowTimeDto>> ExecuteAsync(CancellationToken cancellationToken)
    {
        IReadOnlyList<ShowTime> showTimes = await _showTimeRepository.GetAllAsync(cancellationToken);
        IReadOnlyList<Movie> movies = await _movieRepository.GetAllAsync(cancellationToken);
        var titlesByMovieId = movies.ToDictionary(m => m.Id, m => m.Title);

        return showTimes
            .OrderBy(st => st.StartTime)
            .Select(st => ShowTimeMapper.ToDto(
                st,
                titlesByMovieId.GetValueOrDefault(st.MovieId, string.Empty),
                ShowTimeListPricing.GetListPrice(st.Slot)))
            .ToList();
    }
}
