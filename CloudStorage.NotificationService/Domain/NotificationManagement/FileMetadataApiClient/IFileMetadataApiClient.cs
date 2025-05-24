namespace CloudStorage.NotificationService.Domain.NotificationManagement.FileMetadataApiClient;

public interface IFileMetadataApiClient
{
    Task<FileMetadataDto> GetFileMetadataAsync(Guid fileMetadataId);
}
