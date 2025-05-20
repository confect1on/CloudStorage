using CloudStorage.NotificationService.Domain.NotificationManagement;
using CloudStorage.NotificationService.Domain.NotificationManagement.ValueObjects;
using MimeKit;

namespace CloudStorage.NotificationService.Notifications;

public interface IEmailBodyFactory
{
    MimeEntity CreateBodyByEventTime(UserDto userDto, EventType eventType);
}
