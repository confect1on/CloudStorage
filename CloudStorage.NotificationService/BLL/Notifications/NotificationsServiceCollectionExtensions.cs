using CloudStorage.NotificationService.BLL.Notifications.EmailNotificationService;
using CloudStorage.NotificationService.Domain.NotificationManagement;
using CloudStorage.NotificationService.Notifications.EmailBodyFactory;
using CloudStorage.NotificationService.Notifications.EmailNotificationService;
using CloudStorage.NotificationService.Notifications.FileUriBuilder;
using MailKit.Net.Smtp;

namespace CloudStorage.NotificationService.BLL.Notifications;

internal static class NotificationsServiceCollectionExtensions
{
    public static IServiceCollection AddNotificationServices(this IServiceCollection services, IConfiguration configuration) => services
        .AddSingleton<IFileUriBuilder, MockFileUriBuilder>()
        .AddSingleton<IEmailBodyFactory, EmailBodyByEventFactory>()
        .Configure<EmailNotificationSettings>(configuration.GetSection(nameof(EmailNotificationSettings)))
        .AddSingleton<INotificationService, EmailNotificationService.EmailNotificationService>()
        .AddSingleton<ISmtpClient, SmtpClient>();
}
