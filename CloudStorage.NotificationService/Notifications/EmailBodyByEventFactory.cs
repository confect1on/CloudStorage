using CloudStorage.NotificationService.Domain.NotificationManagement;
using CloudStorage.NotificationService.Domain.NotificationManagement.ValueObjects;
using MimeKit;

namespace CloudStorage.NotificationService.Notifications;

internal sealed class EmailBodyByEventFactory : IEmailBodyFactory
{
    public MimeEntity CreateBodyByEventTime(UserDto userDto, EventType eventType)
    {
        throw new NotImplementedException();
    }
}
