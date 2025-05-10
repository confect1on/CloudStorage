using CloudStorage.UseCases.GetFile;
using Microsoft.Extensions.DependencyInjection;

namespace CloudStorage.UseCases;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCloudStorageUseCases(this IServiceCollection services) => services
        .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetFileQuery>());
}
