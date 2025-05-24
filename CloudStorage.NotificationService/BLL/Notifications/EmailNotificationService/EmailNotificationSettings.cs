namespace CloudStorage.NotificationService.BLL.Notifications.EmailNotificationService;

public record EmailNotificationSettings
{
    public required string SenderEmail { get; init; }
}
