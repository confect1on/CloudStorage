namespace CloudStorage.NotificationService.BLL.Notifications.FileUriBuilder;

public interface IFileUriBuilder
{
    Uri GetFileUri(Guid fileMetadataId);
}
