using CloudStorage.Domain.Abstractions;
using CloudStorage.Domain.DomainServices;
using Microsoft.Extensions.DependencyInjection;

namespace CloudStorage.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services) => services
        .AddSingleton<IDateTimeOffsetProvider, DateTimeOffsetProvider>()
        .AddSingleton<IUserAccessor, UserAccessor>();
}
