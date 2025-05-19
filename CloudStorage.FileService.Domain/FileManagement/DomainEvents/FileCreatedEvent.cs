using CloudStorage.FileService.Domain.FileManagement.ValueObjects;

namespace CloudStorage.FileService.Domain.FileManagement.DomainEvents;

public record FileCreatedEvent(
    EventId Id,
    DateTimeOffset CreatedAt,
    FileMetadataId FileMetadataId,
    TemporaryStorageId TemporaryStorageId) : IDomainEvent
{
    public string EventGroup => "files";
    public string EventType => "created";
}
