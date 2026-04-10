using CinemaSales.Application.DTOs;

namespace CinemaSales.Application.Abstractions.Services;

public interface ICampaignService
{
    Task<IReadOnlyList<CampaignDto>> GetAllCampaignsAsync(CancellationToken cancellationToken);
}
