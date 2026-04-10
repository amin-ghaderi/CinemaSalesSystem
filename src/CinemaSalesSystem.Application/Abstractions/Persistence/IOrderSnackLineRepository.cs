using CinemaSales.Domain.Aggregates.Orders;

namespace CinemaSales.Application.Abstractions.Persistence;

public interface IOrderSnackLineRepository
{
    Task<IReadOnlyList<OrderSnackLine>> GetAllAsync(CancellationToken cancellationToken);
}
