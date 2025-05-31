using CloudStorage.NotificationService.BLL.Notifications.EmailNotificationService.Dtos;
using CloudStorage.NotificationService.Domain.NotificationManagement.UserApiClient;
using MimeKit;

namespace CloudStorage.NotificationService.BLL.Notifications.EmailBodyFactory;

public interface IEmailBodyFactory
{
    MimeEntity CreateBodyByEventTime(EventDto eventDto, UserDto userDto);
}
