using CloudStorage.Domain.Abstractions;

namespace CloudStorage.Domain.DomainServices;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services) => services
        .AddSingleton<IDateTimeOffsetProvider, DateTimeOffsetProvider>();
}
