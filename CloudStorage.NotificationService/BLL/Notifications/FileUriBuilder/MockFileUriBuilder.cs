namespace CloudStorage.NotificationService.Notifications.FileUriBuilder;

// TODO: add service discovery to determine frontend URI
internal sealed class MockFileUriBuilder : IFileUriBuilder
{
    public Uri GetFileUri(Guid fileMetadataId)
    {
        return new Uri($"https://localhost:5000/files/{fileMetadataId}");
    }
}
