namespace CinemaSales.Domain.Enums;

/// <summary>
/// Lifecycle state of a customer order.
/// </summary>
public enum OrderStatus
{
    /// <summary>Order created; payment not completed.</summary>
    Pending = 0,

    /// <summary>Order successfully paid.</summary>
    Paid = 1,

    /// <summary>Order cancelled before payment.</summary>
    Cancelled = 2,

    /// <summary>Order refunded after payment.</summary>
    Refunded = 3
}
