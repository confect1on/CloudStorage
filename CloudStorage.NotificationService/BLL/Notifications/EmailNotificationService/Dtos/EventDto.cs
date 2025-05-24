using CloudStorage.NotificationService.Domain.NotificationManagement.ValueObjects;

namespace CloudStorage.NotificationService.Notifications.EmailNotificationService.Dtos;

public record EventDto(EventType EventType, Guid FileMetadataId);
