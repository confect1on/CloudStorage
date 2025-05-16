namespace CloudStorage.Domain;

public interface IDomainEvent
{
    EventId Id { get; }

    DateTimeOffset CreatedAt { get; }
    
    string Key { get; }

    string EventGroup { get; }

    string EventType { get; }
}
