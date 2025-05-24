namespace CloudStorage.NotificationService.Notifications.FileUriBuilder;

public interface IFileUriBuilder
{
    Uri GetFileUri(Guid fileMetadataId);
}
