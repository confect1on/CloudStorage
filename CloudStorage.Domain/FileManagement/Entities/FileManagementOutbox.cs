using CloudStorage.Domain.FileManagement.ValueObjects;

namespace CloudStorage.Domain.FileManagement.Entities;

public record FileManagementOutbox(
    FileManagementOutboxId Id,
    string Type,
    string Content,
    DateTimeOffset CreatedAt,
    DateTimeOffset? ProcessedAt,
    string? ErrorMessage);

