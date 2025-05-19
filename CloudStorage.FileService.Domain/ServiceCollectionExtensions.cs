using CloudStorage.FileService.Domain.Abstractions;
using CloudStorage.FileService.Domain.DomainServices;
using Microsoft.Extensions.DependencyInjection;

namespace CloudStorage.FileService.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services) => services
        .AddSingleton<IDateTimeOffsetProvider, DateTimeOffsetProvider>()
        .AddSingleton<IUserAccessor, UserAccessor>();
}
