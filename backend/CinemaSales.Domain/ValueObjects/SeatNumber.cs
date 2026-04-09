using CinemaSales.Domain.Common;

namespace CinemaSales.Domain.ValueObjects;

/// <summary>
/// Identifies a seat by auditorium row and seat number.
/// </summary>
public sealed record SeatNumber
{
    /// <summary>
    /// Initializes a new instance of <see cref="SeatNumber"/>.
    /// </summary>
    /// <param name="row">The row label (e.g. A, B).</param>
    /// <param name="number">The seat number within the row.</param>
    public SeatNumber(string row, int number)
    {
        Row = Guard.AgainstNullOrWhiteSpace(row, nameof(row)).Trim().ToUpperInvariant();
        Number = Guard.AgainstNonPositive(number, nameof(number));
    }

    /// <summary>
    /// Gets the row label.
    /// </summary>
    public string Row { get; }

    /// <summary>
    /// Gets the seat number within the row.
    /// </summary>
    public int Number { get; }

    /// <inheritdoc />
    public override string ToString() => $"{Row}{Number}";
}
