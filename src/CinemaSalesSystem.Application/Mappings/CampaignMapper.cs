using CinemaSales.Application.DTOs;
using CinemaSales.Domain.Entities;

namespace CinemaSales.Application.Mappings;

public static class CampaignMapper
{
    public static CampaignDto ToDto(Campaign campaign)
    {
        ArgumentNullException.ThrowIfNull(campaign);

        return new CampaignDto
        {
            Id = campaign.Id,
            Name = campaign.Name,
            Description =
                $"Code {campaign.DiscountCode}. Valid through {campaign.ValidUntil:yyyy-MM-dd} UTC.",
            DiscountPercentage = campaign.DiscountPercentage
        };
    }
}
