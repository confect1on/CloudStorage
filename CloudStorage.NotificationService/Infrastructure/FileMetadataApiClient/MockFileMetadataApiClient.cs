using CloudStorage.NotificationService.Domain.NotificationManagement.FileMetadataApiClient;

namespace CloudStorage.NotificationService.FileMetadataApiClient;

internal sealed class MockFileMetadataApiClient : IFileMetadataApiClient
{
    public Task<FileMetadataDto> GetFileMetadataAsync(Guid fileMetadataId)
    {
        throw new NotImplementedException();
    }
}
