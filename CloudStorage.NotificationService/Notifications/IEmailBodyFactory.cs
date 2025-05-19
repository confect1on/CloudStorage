using CloudStorage.NotificationService.Domain.NotificationManagement.ValueObjects;

namespace CloudStorage.NotificationService.Notifications;

public interface IEmailBodyFactory
{
    string CreateBodyByEventTime(EventType eventType);
}
