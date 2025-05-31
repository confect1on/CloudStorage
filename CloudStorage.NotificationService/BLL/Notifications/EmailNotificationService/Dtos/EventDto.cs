using CloudStorage.NotificationService.Domain.NotificationManagement.ValueObjects;

namespace CloudStorage.NotificationService.BLL.Notifications.EmailNotificationService.Dtos;

public record EventDto(EventType EventType, Guid FileMetadataId);
