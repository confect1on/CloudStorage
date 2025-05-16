using CloudStorage.Domain.FileManagement.ValueObjects;

namespace CloudStorage.Domain.FileManagement.DomainEvents;

public record FileDeletedEvent(
    EventId Id,
    DateTimeOffset CreatedAt,
    FileMetadataId AggregateId) : IDomainEvent
{
    public string Key => $"{EventGroup}.{EventType}";
    
    public string EventType => "deleted";
    
    public string EventGroup => $"files.{AggregateId}";
}
