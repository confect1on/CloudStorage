using CloudStorage.Domain.FileManagement.ValueObjects;

namespace CloudStorage.Domain.FileManagement.DomainEvents;

public record FileDeletedEvent(
    EventId Id,
    DateTimeOffset CreatedAt,
    FileMetadataId AggregateId) : IDomainEvent<FileMetadataId>;
