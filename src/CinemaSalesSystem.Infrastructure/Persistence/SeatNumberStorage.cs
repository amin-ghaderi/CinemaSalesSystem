using System.Globalization;

using CinemaSales.Domain.ValueObjects;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CinemaSales.Infrastructure.Persistence;

internal static class SeatNumberStorage
{
    public static string Serialize(SeatNumber value) =>
        $"{value.Row}|{value.Number.ToString(CultureInfo.InvariantCulture)}";

    public static SeatNumber Deserialize(string value)
    {
        var separator = value.IndexOf('|', StringComparison.Ordinal);
        if (separator < 0)
        {
            throw new InvalidOperationException("Invalid persisted SeatNumber format.");
        }

        var row = value[..separator];
        var numberPart = value[(separator + 1)..];
        return new SeatNumber(row, int.Parse(numberPart, CultureInfo.InvariantCulture));
    }

    public static ValueConverter<SeatNumber, string> Converter { get; } = new(
        v => Serialize(v),
        v => Deserialize(v));
}
