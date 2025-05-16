namespace CloudStorage.Domain;

public interface IDomainEvent
{
    EventId Id { get; }

    DateTimeOffset CreatedAt { get; }
}
