using Amazon.S3;
using CloudStorage.Domain.FileManagement.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CloudStorage.Infrastructure.S3Storage;

public static class S3ServiceCollectionExtensions
{
    public static IServiceCollection AddS3Storage(this IServiceCollection services, IConfiguration configuration) => services
        .ConfigureFileStorage(configuration)
        .AddFileStorage(configuration);
    
        
    internal static IServiceCollection ConfigureFileStorage(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .Configure<S3FileStorageSettings>(configuration.GetSection(nameof(S3FileStorageSettings)));
    }
    
    internal static IServiceCollection AddFileStorage(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDefaultAWSOptions(configuration.GetAWSOptions($"{nameof(S3FileStorageSettings)}:AWS"))
            .AddAWSService<IAmazonS3>()
            .AddSingleton<IFileStorage, S3FileStorage>()
            .AddSingleton<ITemporaryFileStorage, S3FileStorage>()
            .AddAsyncInitializer<S3FileStorageAsyncInitializer>();
    }
}
