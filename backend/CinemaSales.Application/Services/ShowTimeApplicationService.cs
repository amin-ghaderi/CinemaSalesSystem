using CinemaSales.Application.Abstractions.Services;
using CinemaSales.Application.DTOs;
using CinemaSales.Application.UseCases;

namespace CinemaSales.Application.Services;

public sealed class ShowTimeApplicationService : IShowTimeService
{
    private readonly GetAllShowTimesUseCase _getAllShowTimes;
    private readonly GetShowTimesByMovieIdUseCase _getByMovieId;
    private readonly GetShowTimesByCinemaIdUseCase _getByCinemaId;

    public ShowTimeApplicationService(
        GetAllShowTimesUseCase getAllShowTimes,
        GetShowTimesByMovieIdUseCase getByMovieId,
        GetShowTimesByCinemaIdUseCase getByCinemaId)
    {
        _getAllShowTimes = getAllShowTimes;
        _getByMovieId = getByMovieId;
        _getByCinemaId = getByCinemaId;
    }

    public Task<IReadOnlyList<ShowTimeDto>> GetAllShowTimesAsync(CancellationToken cancellationToken) =>
        _getAllShowTimes.ExecuteAsync(cancellationToken);

    public Task<IReadOnlyList<ShowTimeDto>> GetShowTimesByMovieIdAsync(
        Guid movieId,
        CancellationToken cancellationToken) =>
        _getByMovieId.ExecuteAsync(movieId, cancellationToken);

    public Task<IReadOnlyList<ShowTimeDto>> GetShowTimesByCinemaIdAsync(
        Guid cinemaId,
        CancellationToken cancellationToken) =>
        _getByCinemaId.ExecuteAsync(cinemaId, cancellationToken);
}
