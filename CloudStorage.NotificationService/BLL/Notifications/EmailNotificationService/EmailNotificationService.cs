using CloudStorage.NotificationService.Domain.NotificationManagement;
using CloudStorage.NotificationService.Domain.NotificationManagement.FileMetadataApiClient;
using CloudStorage.NotificationService.Domain.NotificationManagement.UserApiClient;
using CloudStorage.NotificationService.Notifications.EmailBodyFactory;
using CloudStorage.NotificationService.Notifications.EmailNotificationService;
using CloudStorage.NotificationService.Notifications.EmailNotificationService.Dtos;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CloudStorage.NotificationService.BLL.Notifications.EmailNotificationService;

internal sealed class EmailNotificationService(
    IUserApiClient userApiClient,
    IFileMetadataApiClient fileMetadataApiClient,
    ISmtpClient smtpClient,
    IEmailBodyFactory bodyFactory,
    IOptions<EmailNotificationSettings> options) : INotificationService
{
    public async Task SendNotificationAsync(EventNotificationDto eventNotificationDto, CancellationToken cancellationToken)
    {
        var fileMetadataDto = await fileMetadataApiClient.GetFileMetadataAsync(eventNotificationDto.EventDto.FileMetadataId);
        var userDto = await userApiClient.GetUserAsync(fileMetadataDto.UserOwnerId, cancellationToken);
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("sender", options.Value.SenderEmail));
        message.To.Add(new MailboxAddress("recipient", userDto.Email));
        message.Subject = "Cloud Storage Notification Service";
        message.Body = bodyFactory.CreateBodyByEventTime(eventNotificationDto.EventDto, userDto);
        await smtpClient.SendAsync(message, cancellationToken);
    }
}
