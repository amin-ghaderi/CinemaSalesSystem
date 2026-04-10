using CinemaSales.Domain.Common;
using CinemaSales.Domain.Enums;

namespace CinemaSales.Domain.Aggregates.Movies;

/// <summary>
/// A scheduled screening of a <see cref="Movie"/> aggregate.
/// </summary>
public sealed class ShowTime : Entity
{
    /// <summary>
    /// Initializes a show time owned by a movie. Intended for use from <see cref="Movie"/> only.
    /// </summary>
    /// <param name="movieId">Owning movie identifier.</param>
    /// <param name="startTime">Start instant of the screening.</param>
    /// <param name="auditorium">Auditorium or screen identifier.</param>
    /// <param name="slot">Time-of-day classification.</param>
    internal ShowTime(Guid movieId, DateTimeOffset startTime, string auditorium, ShowTimeSlot slot)
        : base()
    {
        MovieId = Guard.AgainstEmpty(movieId, nameof(movieId));
        StartTime = startTime;
        Auditorium = Guard.AgainstNullOrWhiteSpace(auditorium, nameof(auditorium));
        Slot = slot;
    }

    /// <summary>
    /// Initializes a show time with a known identifier (e.g. for rehydration).
    /// </summary>
    public ShowTime(Guid id, Guid movieId, DateTimeOffset startTime, string auditorium, ShowTimeSlot slot)
        : base(id)
    {
        MovieId = Guard.AgainstEmpty(movieId, nameof(movieId));
        StartTime = startTime;
        Auditorium = Guard.AgainstNullOrWhiteSpace(auditorium, nameof(auditorium));
        Slot = slot;
    }

    /// <summary>
    /// Gets the owning movie identifier.
    /// </summary>
    public Guid MovieId { get; private set; }

    /// <summary>
    /// Gets the screening start time.
    /// </summary>
    public DateTimeOffset StartTime { get; private set; }

    /// <summary>
    /// Gets the auditorium identifier.
    /// </summary>
    public string Auditorium { get; private set; }

    /// <summary>
    /// Gets the time-of-day slot.
    /// </summary>
    public ShowTimeSlot Slot { get; private set; }
}
