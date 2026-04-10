namespace CinemaSales.Application.DTOs;

public sealed class SnackDto
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public decimal Price { get; init; }
}
