namespace CinemaSales.Application.DTOs;

public sealed class ShowTimeDto
{
    public Guid Id { get; init; }

    public Guid MovieId { get; init; }

    public string MovieTitle { get; init; } = string.Empty;

    public DateTime StartTime { get; init; }

    public string Auditorium { get; init; } = string.Empty;

    public string Slot { get; init; } = string.Empty;

    public decimal Price { get; init; }
}
