using CloudStorage.Domain.FileManagement.ValueObjects;

namespace CloudStorage.Domain.FileManagement.DomainEvents;

public record FileCreatedEvent(
    EventId Id,
    DateTimeOffset CreatedAt,
    FileMetadataId AggregateId,
    TemporaryStorageId TemporaryStorageId) : IDomainEvent
{
    public string EventGroup => $"files";
    public string EventType => "created";
}
