using CloudStorage.NotificationService.BLL.Notifications.EmailNotificationService.Dtos;
using CloudStorage.NotificationService.Domain.NotificationManagement.ValueObjects;

namespace CloudStorage.NotificationService.Domain.NotificationManagement;

public interface INotificationService
{
    Task SendNotificationAsync(EventNotificationDto eventNotificationDto, CancellationToken cancellationToken = default);
}
