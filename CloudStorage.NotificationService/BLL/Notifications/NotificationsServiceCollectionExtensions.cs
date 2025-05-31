using CloudStorage.NotificationService.BLL.Notifications.EmailBodyFactory;
using CloudStorage.NotificationService.BLL.Notifications.EmailNotificationService;
using CloudStorage.NotificationService.BLL.Notifications.FileUriBuilder;
using CloudStorage.NotificationService.Domain.NotificationManagement;
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
