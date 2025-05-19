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
    IOptions<EmailNotificationSettings> options) : INotificationService
{
    public async Task SendNotificationAsync(Guid userId, EventType eventType, CancellationToken cancellationToken)
    {
        var userDto = await userApiClient.GetUserAsync(userId, cancellationToken);
        // smtpClient.SendMailAsync(options.Value.SenderEmail, userDto.Email, )
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("sender", userDto.Email));
    }
}
