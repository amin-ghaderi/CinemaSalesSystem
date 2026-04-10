using CinemaSales.Application.Abstractions.Services;
using CinemaSales.ConsoleUI.Models;

namespace CinemaSales.ConsoleUI.Services;

public class SnackService
{
    private readonly ISnackService _snackService;

    public SnackService(ISnackService snackService)
    {
        _snackService = snackService;
    }

    public async Task<IEnumerable<SnackViewModel>> GetSnacksAsync()
    {
        var snacks = await _snackService.GetAllSnacksAsync(CancellationToken.None);

        return snacks.Select(s => new SnackViewModel
        {
            Id = s.Id,
            Name = s.Name,
            Price = s.Price
        });
    }

    public async Task<(string Name, int Quantity, decimal UnitPrice, decimal TotalPrice)>
        PurchaseSnackAsync(Guid snackId, int quantity)
    {
        var result = await _snackService.PurchaseSnackAsync(
            snackId,
            quantity,
            CancellationToken.None);

        return (result.SnackName, result.Quantity, result.UnitPrice, result.TotalPrice);
    }
}
