using CinemaSales.Application.Abstractions.Persistence;
using CinemaSales.Application.DTOs;
using CinemaSales.Application.Mappings;
using CinemaSales.Domain.Aggregates.Movies;

namespace CinemaSales.Application.UseCases;

public sealed class GetShowTimesByMovieIdUseCase
{
    private readonly IShowTimeRepository _showTimeRepository;

    public GetShowTimesByMovieIdUseCase(IShowTimeRepository showTimeRepository)
    {
        _showTimeRepository = showTimeRepository;
    }

    public async Task<IReadOnlyList<ShowTimeDto>> ExecuteAsync(
        Guid movieId,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<ShowTime> showTimes = await _showTimeRepository
            .GetByMovieIdAsync(movieId, cancellationToken);

        return showTimes
            .Select(ShowTimeMapper.ToDto)
            .ToList();
    }
}
