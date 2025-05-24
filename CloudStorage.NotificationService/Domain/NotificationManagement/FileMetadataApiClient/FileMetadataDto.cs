namespace CloudStorage.NotificationService.Domain.NotificationManagement.FileMetadataApiClient;

public record FileMetadataDto
{
    public Guid UserOwnerId { get; init; }
}
