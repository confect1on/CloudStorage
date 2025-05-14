using CloudStorage.Domain.Entities.Ids;

namespace CloudStorage.Domain.Entities;

public record FileMetadataOutbox(
    EventId EventId,
    FileMetadataId FileMetadataId,
    OutboxStatus OutboxStatus,
    DateTimeOffset CreatedAt);

