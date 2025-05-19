namespace CloudStorage.NotificationService.Notifications;

public record EmailNotificationSettings
{
    public required string SenderEmail { get; init; }
}
