namespace CinemaSales.Domain.Common;

/// <summary>
/// Base type for all domain events.
/// </summary>
public abstract record BaseDomainEvent
{
    /// <summary>
    /// Gets the UTC timestamp when the event occurred.
    /// </summary>
    public DateTimeOffset OccurredOn { get; init; } = DateTimeOffset.UtcNow;
}
