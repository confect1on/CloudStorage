using CloudStorage.FileService.Domain.FileManagement.ValueObjects;

namespace CloudStorage.FileService.Domain.FileManagement.Entities;

public class FileManagementOutbox
{
    public FileManagementOutboxId Id { get; init; }

    public required string Type { get; init; }

    public required string Content { get; init; }

    public DateTimeOffset CreatedAt { get; init; }

    public DateTimeOffset? ProcessedAt { get; init; }

    public string? ErrorMessage { get; init; }
}

