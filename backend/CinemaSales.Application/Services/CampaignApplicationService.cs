using CinemaSales.Application.Abstractions.Services;
using CinemaSales.Application.DTOs;
using CinemaSales.Application.UseCases;

namespace CinemaSales.Application.Services;

public sealed class CampaignApplicationService : ICampaignService
{
    private readonly GetAllCampaignsUseCase _getAllCampaigns;

    public CampaignApplicationService(GetAllCampaignsUseCase getAllCampaigns)
    {
        _getAllCampaigns = getAllCampaigns;
    }

    public Task<IReadOnlyList<CampaignDto>> GetAllCampaignsAsync(CancellationToken cancellationToken) =>
        _getAllCampaigns.ExecuteAsync(cancellationToken);
}
