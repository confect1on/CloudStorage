namespace CloudStorage.Domain;

public interface IDomainEvent<out TAggregateId>
{
    EventId Id { get; }

    DateTimeOffset CreatedAt { get; }
    
    TAggregateId AggregateId { get; }
}
