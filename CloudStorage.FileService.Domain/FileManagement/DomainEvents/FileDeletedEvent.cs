using CloudStorage.FileService.Domain.FileManagement.ValueObjects;

namespace CloudStorage.FileService.Domain.FileManagement.DomainEvents;

public record FileDeletedEvent(
    EventId Id,
    DateTimeOffset CreatedAt,
    FileMetadataId FileMetadataId) : IDomainEvent
{
    public string EventGroup => "files";

    public string EventType => "deleted";
}
