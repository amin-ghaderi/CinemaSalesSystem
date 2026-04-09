namespace CinemaSales.Domain.Enums;

/// <summary>
/// Defines the pricing category of a cinema ticket.
/// </summary>
public enum TicketType
{
    /// <summary>Standard adult admission.</summary>
    Regular = 0,

    /// <summary>Discounted student admission.</summary>
    Student = 1,

    /// <summary>Child admission.</summary>
    Child = 2,

    /// <summary>Senior admission.</summary>
    Senior = 3,

    /// <summary>VIP admission.</summary>
    Vip = 4
}
