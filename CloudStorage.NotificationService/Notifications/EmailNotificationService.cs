using System.Net.Mail;
using CloudStorage.NotificationService.Domain.NotificationManagement;
using CloudStorage.NotificationService.Domain.NotificationManagement.ValueObjects;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CloudStorage.NotificationService.Notifications;

internal sealed class EmailNotificationService(
    IUserApiClient userApiClient,
    ISmtpClient smtpClient,
    IEmailBodyFactory bodyFactory,
    IOptions<EmailNotificationSettings> options) : INotificationService
{
    public async Task SendNotificationAsync(Guid userId, EventType eventType, CancellationToken cancellationToken)
    {
        var userDto = await userApiClient.GetUserAsync(userId, cancellationToken);
        // smtpClient.SendMailAsync(options.Value.SenderEmail, userDto.Email, )
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("sender", options.Value.SenderEmail));
        message.To.Add(new MailboxAddress("recipient", userDto.Email));
        message.Subject = "Cloud Storage Notification Service";
        message.Body = bodyFactory.CreateBodyByEventTime(userDto, eventType);
        await smtpClient.SendAsync(message, cancellationToken);
    }
}
