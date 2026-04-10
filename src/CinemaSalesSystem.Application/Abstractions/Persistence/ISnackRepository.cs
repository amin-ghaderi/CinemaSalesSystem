using CinemaSales.Domain.Entities;

namespace CinemaSales.Application.Abstractions.Persistence;

public interface ISnackRepository
{
    Task<Snack?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyList<Snack>> GetAllAsync(CancellationToken cancellationToken);
}
