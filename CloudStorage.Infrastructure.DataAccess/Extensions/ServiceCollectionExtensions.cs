using CloudStorage.Infrastructure.DataAccess.Migrations;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CloudStorage.Infrastructure.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccessInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDataAccessOptions(configuration)
            .AddMigrations();
    }

    private static IServiceCollection AddMigrations(this IServiceCollection services) => services.AddFluentMigratorCore()
        .ConfigureRunner(
            builder => builder
                .AddPostgres()
                .WithGlobalConnectionString(
                    s =>
                    {
                        var dalOptions = s.GetRequiredService<IOptions<DalOptions>>();
                        return dalOptions.Value.PostgresConnectionString;
                    })
                .ScanIn(typeof(AddFileMetadataTable).Assembly).For.Migrations());

    private static IServiceCollection AddDataAccessOptions(this IServiceCollection services, IConfiguration configuration) => services
        .Configure<DalOptions>(configuration.GetSection(nameof(DalOptions)));
}
