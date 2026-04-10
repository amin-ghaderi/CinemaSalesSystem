using CinemaSales.Application.Abstractions.Persistence;
using CinemaSales.Application.DTOs;
using CinemaSales.Domain.Entities;

namespace CinemaSales.Application.UseCases;

/// <summary>
/// Validates snack catalog purchase and returns line totals (net unit price × quantity).
/// </summary>
public sealed class PurchaseSnackUseCase
{
    private readonly ISnackRepository _snackRepository;

    public PurchaseSnackUseCase(ISnackRepository snackRepository)
    {
        _snackRepository = snackRepository;
    }

    public async Task<SnackPurchaseResultDto> ExecuteAsync(
        Guid snackId,
        int quantity,
        CancellationToken cancellationToken)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be positive.");
        }

        Snack? snack = await _snackRepository.GetByIdAsync(snackId, cancellationToken) ?? throw new InvalidOperationException("Snack was not found.");
        decimal unit = snack.Price.Amount;
        decimal total = decimal.Round(unit * quantity, 2, MidpointRounding.AwayFromZero);

        return new SnackPurchaseResultDto
        {
            SnackName = snack.Name,
            Quantity = quantity,
            UnitPrice = unit,
            TotalPrice = total
        };
    }
}
