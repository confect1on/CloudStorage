using CloudStorage.Domain.Abstractions;
using CloudStorage.Domain.Entities.Ids;
using CloudStorage.Infrastructure.DataAccess.Migrations;
using Dapper;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql;
using Npgsql.NameTranslation;

namespace CloudStorage.Infrastructure.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    private static readonly INpgsqlNameTranslator Translator = new NpgsqlSnakeCaseNameTranslator();

    public static IServiceCollection AddDataAccessInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        SqlMapper.AddTypeHandler(new FileMetadataId.DapperTypeHandler());
        SqlMapper.AddTypeHandler(new StorageId.DapperTypeHandler());
        SqlMapper.AddTypeHandler(new UserId.DapperTypeHandler());
        return services
            .AddDataAccessOptions(configuration)
            .AddMigrations()
            .AddServices();
    }
    private static IServiceCollection AddMigrations(this IServiceCollection services) => services.AddFluentMigratorCore()
        .ConfigureRunner(
            builder => builder
                .AddPostgres()
                .WithGlobalConnectionString(
                    s =>
                    {
                        var dalOptions = s.GetRequiredService<IOptions<DalSettings>>();
                        return dalOptions.Value.PostgresConnectionString;
                    })
                .ScanIn(typeof(AddFileMetadataTable).Assembly).For.Migrations());

    private static IServiceCollection AddDataAccessOptions(this IServiceCollection services, IConfiguration configuration) => services
        .Configure<DalSettings>(configuration.GetSection(nameof(DalSettings)));

    private static IServiceCollection AddServices(this IServiceCollection services) => services
        .AddSingleton<IFileStorage, InMemoryFileStorage>()
        .AddSingleton<IFileMetadataRepository, FileMetadataRepository>();
}
