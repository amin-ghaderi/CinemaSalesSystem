using CinemaSales.Application.Abstractions.Services;
using CinemaSales.ConsoleUI.Models;

namespace CinemaSales.ConsoleUI.Services;

public class CampaignService
{
    private readonly ICampaignService _campaignService;

    public CampaignService(ICampaignService campaignService)
    {
        _campaignService = campaignService;
    }

    public async Task<IEnumerable<CampaignViewModel>> GetCampaignsAsync()
    {
        var campaigns = await _campaignService.GetAllCampaignsAsync(CancellationToken.None);

        return campaigns.Select(c => new CampaignViewModel
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            DiscountPercentage = c.DiscountPercentage
        });
    }
}
