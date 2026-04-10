using CinemaSales.Domain.Entities;

namespace CinemaSales.Application.Abstractions.Persistence;

public interface ICampaignRepository
{
    Task<IReadOnlyList<Campaign>> GetAllAsync(CancellationToken cancellationToken);
}
