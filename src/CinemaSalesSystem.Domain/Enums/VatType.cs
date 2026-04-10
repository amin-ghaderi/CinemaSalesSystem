namespace CinemaSales.Domain.Enums;

/// <summary>
/// VAT classification for concession items.
/// </summary>
public enum VatType
{
    /// <summary>No VAT applies.</summary>
    None = 0,

    /// <summary>Reduced VAT rate.</summary>
    Reduced = 1,

    /// <summary>Standard VAT rate.</summary>
    Standard = 2
}
