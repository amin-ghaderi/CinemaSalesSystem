using CinemaSales.Application.Abstractions.Services;
using CinemaSales.Application.DTOs;
using CinemaSales.Application.UseCases;

namespace CinemaSales.Application.Services;

public sealed class SnackApplicationService : ISnackService
{
    private readonly GetAllSnacksUseCase _getAllSnacks;
    private readonly PurchaseSnackUseCase _purchaseSnack;

    public SnackApplicationService(
        GetAllSnacksUseCase getAllSnacks,
        PurchaseSnackUseCase purchaseSnack)
    {
        _getAllSnacks = getAllSnacks;
        _purchaseSnack = purchaseSnack;
    }

    public Task<IReadOnlyList<SnackDto>> GetAllSnacksAsync(CancellationToken cancellationToken) =>
        _getAllSnacks.ExecuteAsync(cancellationToken);

    public Task<SnackPurchaseResultDto> PurchaseSnackAsync(
        Guid snackId,
        int quantity,
        CancellationToken cancellationToken) =>
        _purchaseSnack.ExecuteAsync(snackId, quantity, cancellationToken);
}
