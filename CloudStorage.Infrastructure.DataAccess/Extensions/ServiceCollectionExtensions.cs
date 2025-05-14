using CloudStorage.Infrastructure.DataAccess.Persistence;
using CloudStorage.Infrastructure.DataAccess.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CloudStorage.Infrastructure.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccessInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddPersistenceServices(configuration)
            .AddS3StorageServices(configuration);
    }
}
