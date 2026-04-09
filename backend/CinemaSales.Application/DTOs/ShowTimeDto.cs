namespace CinemaSales.Application.DTOs;

public sealed class ShowTimeDto
{
    public Guid Id { get; init; }
    public Guid MovieId { get; init; }
    public DateTime StartTime { get; init; }
    public string Auditorium { get; init; } = string.Empty;
    public string Slot { get; init; } = string.Empty;
}
