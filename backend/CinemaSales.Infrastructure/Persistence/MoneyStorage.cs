using System.Globalization;
using CinemaSales.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CinemaSales.Infrastructure.Persistence;

internal static class MoneyStorage
{
    public static string? Serialize(Money? value) =>
        value is null
            ? null
            : $"{value.Amount.ToString(CultureInfo.InvariantCulture)}|{value.Currency}";

    public static Money? Deserialize(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        var separator = value.IndexOf('|', StringComparison.Ordinal);
        if (separator < 0)
        {
            throw new InvalidOperationException("Invalid persisted Money format.");
        }

        var amountPart = value[..separator];
        var currencyPart = value[(separator + 1)..];
        return new Money(decimal.Parse(amountPart, CultureInfo.InvariantCulture), currencyPart);
    }

    public static ValueConverter<Money?, string?> NullableConverter { get; } = new(
        v => Serialize(v),
        v => Deserialize(v));

    public static Money DeserializeRequired(string value) =>
        Deserialize(value)
        ?? throw new InvalidOperationException("Required Money value is missing.");

    public static ValueConverter<Money, string> RequiredConverter { get; } = new(
        v => Serialize(v)!,
        v => DeserializeRequired(v));
}
