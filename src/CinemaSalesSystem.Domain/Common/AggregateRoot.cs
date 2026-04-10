namespace CinemaSales.Domain.Common;

/// <summary>
/// Base type for aggregate roots that can raise domain events.
/// </summary>
public abstract class AggregateRoot : Entity
{
    private readonly List<BaseDomainEvent> _domainEvents = new();

    /// <summary>
    /// Initializes a new aggregate root with a generated identifier.
    /// </summary>
    protected AggregateRoot()
    {
    }

    /// <summary>
    /// Initializes a new aggregate root with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier.</param>
    protected AggregateRoot(Guid id)
        : base(id)
    {
    }

    /// <summary>
    /// Gets the domain events raised by this aggregate that have not yet been dispatched.
    /// </summary>
    public IReadOnlyCollection<BaseDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Removes all collected domain events (typically after successful dispatch).
    /// </summary>
    public void ClearDomainEvents() => _domainEvents.Clear();

    /// <summary>
    /// Records a domain event raised by this aggregate.
    /// </summary>
    /// <param name="domainEvent">The event instance.</param>
    protected void RaiseDomainEvent(BaseDomainEvent domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);
        _domainEvents.Add(domainEvent);
    }
}
