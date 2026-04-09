namespace CinemaSales.Domain.Enums;

/// <summary>
/// Time-of-day bucket for a scheduled screening.
/// </summary>
public enum ShowTimeSlot
{
    /// <summary>Morning screenings.</summary>
    Morning = 0,

    /// <summary>Afternoon screenings.</summary>
    Afternoon = 1,

    /// <summary>Evening screenings.</summary>
    Evening = 2,

    /// <summary>Late night screenings.</summary>
    Night = 3
}
