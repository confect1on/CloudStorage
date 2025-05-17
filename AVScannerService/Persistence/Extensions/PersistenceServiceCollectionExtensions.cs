using AVScannerService.Persistence.Abstractions;
using AVScannerService.Persistence.Migrations;
using FluentMigrator.Runner;
using Microsoft.Extensions.Options;

namespace AVScannerService.Persistence.Extensions;

internal static class PersistenceServiceCollectionExtensions
{
    internal static IServiceCollection AddPersistenceServices(this IServiceCollection services) => services
        .AddSingleton<IFileCreatedEventInboxRepository, FileCreatedEventInboxRepository>();
    
    internal static IServiceCollection ConfigurePersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration) => services
        .Configure<PersistenceSettings>(configuration.GetSection(nameof(PersistenceSettings)));

    internal static IServiceCollection AddMigrations(this IServiceCollection services) => services.AddFluentMigratorCore()
        .ConfigureRunner(
            builder => builder
                .AddPostgres()
                .WithGlobalConnectionString(
                    s =>
                    {
                        var dalOptions = s.GetRequiredService<IOptions<PersistenceSettings>>();
                        return dalOptions.Value.PostgresConnectionString;
                    })
                .ScanIn(typeof(AddFileCreatedEventInboxTable).Assembly).For.Migrations());
}
