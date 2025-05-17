using CloudStorage.Domain.FileManagement.ValueObjects;

namespace CloudStorage.Domain.FileManagement.DomainEvents;

public record FileDeletedEvent(
    EventId Id,
    DateTimeOffset CreatedAt,
    FileMetadataId AggregateId) : IDomainEvent
{
    public string EventGroup => "files";

    public string EventType => "deleted";
}
