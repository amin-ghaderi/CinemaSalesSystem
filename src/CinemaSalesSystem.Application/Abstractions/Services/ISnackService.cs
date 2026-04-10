using CinemaSales.Application.DTOs;

namespace CinemaSales.Application.Abstractions.Services;

public interface ISnackService
{
    Task<IReadOnlyList<SnackDto>> GetAllSnacksAsync(CancellationToken cancellationToken);

    Task<SnackPurchaseResultDto> PurchaseSnackAsync(
        Guid snackId,
        int quantity,
        CancellationToken cancellationToken);
}
