using CloudStorage.NotificationService.Domain.NotificationManagement.ValueObjects;

namespace CloudStorage.NotificationService.Domain.NotificationManagement;

public interface INotificationService
{
    Task SendNotificationAsync(Guid userId, EventType eventType, CancellationToken cancellationToken);
}
