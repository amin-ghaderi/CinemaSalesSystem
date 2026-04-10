namespace CinemaSales.Application.DTOs;

public sealed class MovieDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Genre { get; init; } = string.Empty;
    public int DurationMinutes { get; init; }
    public string Rating { get; init; } = string.Empty;
}
