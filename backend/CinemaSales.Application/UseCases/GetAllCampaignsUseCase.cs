using CinemaSales.Application.Abstractions.Persistence;
using CinemaSales.Application.DTOs;
using CinemaSales.Application.Mappings;
using CinemaSales.Domain.Entities;

namespace CinemaSales.Application.UseCases;

public sealed class GetAllCampaignsUseCase
{
    private readonly ICampaignRepository _campaignRepository;

    public GetAllCampaignsUseCase(ICampaignRepository campaignRepository)
    {
        _campaignRepository = campaignRepository;
    }

    public async Task<IReadOnlyList<CampaignDto>> ExecuteAsync(CancellationToken cancellationToken)
    {
        IReadOnlyList<Campaign> campaigns = await _campaignRepository.GetAllAsync(cancellationToken);
        return campaigns.Select(CampaignMapper.ToDto).ToList();
    }
}
