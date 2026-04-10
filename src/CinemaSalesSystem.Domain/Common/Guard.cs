namespace CinemaSales.Domain.Common;

/// <summary>
/// Provides lightweight guard clauses for domain invariants.
/// </summary>
public static class Guard
{
    /// <summary>
    /// Throws <see cref="ArgumentNullException"/> when <paramref name="value"/> is null.
    /// </summary>
    public static T AgainstNull<T>(T? value, string paramName)
        where T : class
    {
        ArgumentNullException.ThrowIfNull(value, paramName);
        return value;
    }

    /// <summary>
    /// Throws <see cref="ArgumentException"/> when the string is null or whitespace.
    /// </summary>
    public static string AgainstNullOrWhiteSpace(string? value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", paramName);
        }

        return value;
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> when <paramref name="value"/> is negative.
    /// </summary>
    public static decimal AgainstNegative(decimal value, string paramName)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(paramName, value, "Value cannot be negative.");
        }

        return value;
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> when <paramref name="value"/> is negative.
    /// </summary>
    public static int AgainstNegative(int value, string paramName)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(paramName, value, "Value cannot be negative.");
        }

        return value;
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> when <paramref name="value"/> is not positive.
    /// </summary>
    public static int AgainstNonPositive(int value, string paramName)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(paramName, value, "Value must be greater than zero.");
        }

        return value;
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> when <paramref name="value"/> is the default value.
    /// </summary>
    public static Guid AgainstEmpty(Guid value, string paramName)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentOutOfRangeException(paramName, "Value cannot be an empty GUID.");
        }

        return value;
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> when the percentage is outside 0–100.
    /// </summary>
    public static decimal AgainstInvalidPercentage(decimal percentage, string paramName)
    {
        if (percentage is < 0 or > 100)
        {
            throw new ArgumentOutOfRangeException(paramName, percentage, "Percentage must be between 0 and 100.");
        }

        return percentage;
    }
}
