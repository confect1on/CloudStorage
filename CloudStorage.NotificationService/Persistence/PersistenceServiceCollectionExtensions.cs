using CloudStorage.NotificationService.Persistence.Migrations;
using FluentMigrator.Runner;
using Microsoft.Extensions.Options;

namespace CloudStorage.NotificationService.Persistence;

public static class PersistenceServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration) => services
        .ConfigurePersistence(configuration)
        .AddMigrations();

    private static IServiceCollection ConfigurePersistence(this IServiceCollection services, IConfiguration configuration) => services
        .Configure<PersistenceSettings>(configuration.GetSection(nameof(PersistenceSettings)));
    private static IServiceCollection AddMigrations(this IServiceCollection services) => services
        .AddFluentMigratorCore()
        .ConfigureRunner(
            builder => builder
                .AddPostgres()
                .WithGlobalConnectionString(
                    s =>
                    {
                        var persistenceOptions = s.GetRequiredService<IOptions<PersistenceSettings>>();
                        return persistenceOptions.Value.ConnectionString;
                    })
                .ScanIn(typeof(AddFilePublishedEventInboxTable).Assembly).For.Migrations());
}
