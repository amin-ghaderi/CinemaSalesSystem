using CinemaSales.Application.Abstractions.Persistence;
using CinemaSales.Application.DTOs;
using CinemaSales.Application.Mappings;
using CinemaSales.Application.Pricing;
using CinemaSales.Domain.Aggregates.Movies;

namespace CinemaSales.Application.UseCases;

public sealed class GetShowTimesByMovieIdUseCase
{
    private readonly IShowTimeRepository _showTimeRepository;
    private readonly IMovieRepository _movieRepository;

    public GetShowTimesByMovieIdUseCase(
        IShowTimeRepository showTimeRepository,
        IMovieRepository movieRepository)
    {
        _showTimeRepository = showTimeRepository;
        _movieRepository = movieRepository;
    }

    public async Task<IReadOnlyList<ShowTimeDto>> ExecuteAsync(
        Guid movieId,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<ShowTime> showTimes = await _showTimeRepository
            .GetByMovieIdAsync(movieId, cancellationToken);

        Movie? movie = await _movieRepository.GetByIdAsync(movieId, cancellationToken);
        string title = movie?.Title ?? string.Empty;

        return showTimes
            .Select(st => ShowTimeMapper.ToDto(
                st,
                title,
                ShowTimeListPricing.GetListPrice(st.Slot)))
            .ToList();
    }
}
