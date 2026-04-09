namespace CinemaSales.Domain.Enums;

/// <summary>
/// Content rating for a movie.
/// </summary>
public enum MovieRating
{
    /// <summary>General audiences.</summary>
    G = 0,

    /// <summary>Parental guidance suggested.</summary>
    Pg = 1,

    /// <summary>Parents strongly cautioned.</summary>
    Pg13 = 2,

    /// <summary>Restricted.</summary>
    R = 3,

    /// <summary>Adults only.</summary>
    Nc17 = 4
}
