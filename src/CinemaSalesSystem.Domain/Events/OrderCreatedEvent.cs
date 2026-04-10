using CinemaSales.Domain.Common;

namespace CinemaSales.Domain.Events;

/// <summary>
/// Raised when a new order aggregate is created.
/// </summary>
/// <param name="OrderId">The identifier of the new order.</param>
public sealed record OrderCreatedEvent(Guid OrderId) : BaseDomainEvent;
