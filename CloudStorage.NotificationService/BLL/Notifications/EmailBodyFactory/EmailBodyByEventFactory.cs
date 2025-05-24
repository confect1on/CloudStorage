using CloudStorage.NotificationService.Domain.NotificationManagement;
using CloudStorage.NotificationService.Domain.NotificationManagement.UserApiClient;
using CloudStorage.NotificationService.Domain.NotificationManagement.ValueObjects;
using CloudStorage.NotificationService.Notifications.EmailNotificationService.Dtos;
using CloudStorage.NotificationService.Notifications.FileUriBuilder;
using MimeKit;

namespace CloudStorage.NotificationService.Notifications.EmailBodyFactory;

internal sealed class EmailBodyByEventFactory(IFileUriBuilder fileUriBuilder) : IEmailBodyFactory
{
    public MimeEntity CreateBodyByEventTime(EventDto eventDto, UserDto userDto)
    {
        var bodyBuilder = new BodyBuilder();
        var greeting = $"Добрый день, {userDto.UserName} !<br><br>";
        const string footer = "<br><br>--<br>С уважением,<br>Команда поддержки CloudStorage";

        var content = eventDto.EventType switch
        {
            EventType.FilePublished => 
                $"Ваш <a href='{fileUriBuilder.GetFileUri(eventDto.FileMetadataId)}'>файл</a> был успешно опубликован на платформе.<br>",
            _ => throw new InvalidOperationException($"Unknown event type: {eventDto.EventType}"),
        };

        bodyBuilder.HtmlBody = $"""
                                <div style="font-family: Arial, sans-serif; font-size: 14px; color: #333;">
                                    {greeting}
                                    <div style="margin: 15px 0;">
                                        {content}
                                    </div>
                                    {footer}
                                </div>
                                """;

        return bodyBuilder.ToMessageBody();
    }
}
