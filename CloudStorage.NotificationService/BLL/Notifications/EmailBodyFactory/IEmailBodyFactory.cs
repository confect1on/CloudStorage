using CloudStorage.NotificationService.Domain.NotificationManagement;
using CloudStorage.NotificationService.Domain.NotificationManagement.UserApiClient;
using CloudStorage.NotificationService.Notifications.EmailNotificationService.Dtos;
using MimeKit;

namespace CloudStorage.NotificationService.Notifications.EmailBodyFactory;

public interface IEmailBodyFactory
{
    MimeEntity CreateBodyByEventTime(EventDto eventDto, UserDto userDto);
}
