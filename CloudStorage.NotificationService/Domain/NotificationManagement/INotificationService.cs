using CloudStorage.NotificationService.Domain.NotificationManagement.ValueObjects;
using CloudStorage.NotificationService.Notifications;
using CloudStorage.NotificationService.Notifications.EmailNotificationService.Dtos;

namespace CloudStorage.NotificationService.Domain.NotificationManagement;

public interface INotificationService
{
    Task SendNotificationAsync(EventNotificationDto eventNotificationDto, CancellationToken cancellationToken = default);
}
