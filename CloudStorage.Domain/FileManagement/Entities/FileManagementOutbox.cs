using CloudStorage.Domain.FileManagement.ValueObjects;

namespace CloudStorage.Domain.FileManagement.Entities;

public record FileManagementOutbox(
    FileManagementOutboxId FileManagementOutboxId,
    FileMetadataId FileMetadataId,
    FileManagementOutboxStatus FileManagementOutboxStatus,
    DateTimeOffset CreatedAt);

