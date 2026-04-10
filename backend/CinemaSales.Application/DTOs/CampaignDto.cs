namespace CinemaSales.Application.DTOs;

public sealed class CampaignDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal DiscountPercentage { get; init; }
}
