using CloudStorage.NotificationService.Domain.NotificationManagement.FileMetadataApiClient;
using CloudStorage.NotificationService.Domain.NotificationManagement.UserApiClient;
using CloudStorage.NotificationService.Infrastructure.FileMetadataApiClient;
using CloudStorage.NotificationService.Infrastructure.UserApiClient;

namespace CloudStorage.NotificationService.Infrastructure;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiClients(this IServiceCollection services) => services
        .AddSingleton<IUserApiClient, MockUserApiClient>()
        .AddSingleton<IFileMetadataApiClient, MockFileMetadataApiClient>();
}
