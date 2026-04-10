using CinemaSales.Domain.Common;
using CinemaSales.Domain.Enums;

namespace CinemaSales.Domain.Aggregates.Movies;

/// <summary>
/// Aggregate root representing a film and its scheduled show times.
/// </summary>
public sealed class Movie : AggregateRoot
{
    private readonly List<ShowTime> _showTimes = new();

    /// <summary>
    /// Initializes a new movie.
    /// </summary>
    /// <param name="title">Movie title.</param>
    /// <param name="durationMinutes">Running time in minutes.</param>
    /// <param name="rating">Content rating.</param>
    /// <param name="description">Synopsis or marketing copy.</param>
    /// <param name="genre">Display genre label (e.g. for listings).</param>
    public Movie(string title, int durationMinutes, MovieRating rating, string? description = null, string genre = "")
        : base()
    {
        Title = Guard.AgainstNullOrWhiteSpace(title, nameof(title));
        DurationMinutes = Guard.AgainstNonPositive(durationMinutes, nameof(durationMinutes));
        Rating = rating;
        Description = description?.Trim() ?? string.Empty;
        Genre = genre?.Trim() ?? string.Empty;
    }

    /// <summary>
    /// Initializes a movie with a known identifier.
    /// </summary>
    public Movie(Guid id, string title, int durationMinutes, MovieRating rating, string? description = null, string genre = "")
        : base(id)
    {
        Title = Guard.AgainstNullOrWhiteSpace(title, nameof(title));
        DurationMinutes = Guard.AgainstNonPositive(durationMinutes, nameof(durationMinutes));
        Rating = rating;
        Description = description?.Trim() ?? string.Empty;
        Genre = genre?.Trim() ?? string.Empty;
    }

    /// <summary>
    /// Gets the title.
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    /// Gets the duration in minutes.
    /// </summary>
    public int DurationMinutes { get; private set; }

    /// <summary>
    /// Gets the content rating.
    /// </summary>
    public MovieRating Rating { get; private set; }

    /// <summary>
    /// Gets the description.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Gets the genre label for display (e.g. in console listings).
    /// </summary>
    public string Genre { get; private set; }

    /// <summary>
    /// Gets show times owned by this movie.
    /// </summary>
    public IReadOnlyCollection<ShowTime> ShowTimes => _showTimes.AsReadOnly();

    /// <summary>
    /// Schedules a new show time for this movie.
    /// </summary>
    /// <param name="startTime">Start instant.</param>
    /// <param name="auditorium">Auditorium identifier.</param>
    /// <param name="slot">Slot classification.</param>
    /// <returns>The created show time.</returns>
    public ShowTime ScheduleShowTime(DateTimeOffset startTime, string auditorium, ShowTimeSlot slot)
    {
        var showTime = new ShowTime(Id, startTime, auditorium, slot);
        _showTimes.Add(showTime);
        return showTime;
    }

    /// <summary>
    /// Removes a show time by identifier.
    /// </summary>
    /// <returns><c>true</c> when a show time was removed.</returns>
    public bool RemoveShowTime(Guid showTimeId) => _showTimes.RemoveAll(st => st.Id == showTimeId) > 0;
}
