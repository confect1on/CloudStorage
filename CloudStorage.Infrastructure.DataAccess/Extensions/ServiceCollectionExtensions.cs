using Amazon.S3;
using CloudStorage.Domain.Abstractions;
using CloudStorage.Domain.Entities.Ids;
using CloudStorage.Infrastructure.DataAccess.Migrations;
using CloudStorage.Infrastructure.DataAccess.S3;
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
            .ConfigureRepositories(configuration)
            .AddMigrations()
            .AddRepositories()
            .ConfigureFileStorage(configuration)
            .AddFileStorage(configuration);
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

    private static IServiceCollection ConfigureRepositories(this IServiceCollection services, IConfiguration configuration) => services
        .Configure<DalSettings>(configuration.GetSection(nameof(DalSettings)));
    
    private static IServiceCollection ConfigureFileStorage(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .Configure<S3FileStorageSettings>(configuration.GetSection(nameof(S3FileStorageSettings)));
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services) => services
        .AddSingleton<IFileMetadataRepository, FileMetadataRepository>();
    
    private static IServiceCollection AddFileStorage(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDefaultAWSOptions(configuration.GetAWSOptions($"{nameof(S3FileStorageSettings)}:AWS"))
            .AddAWSService<IAmazonS3>()
            .AddSingleton<IFileStorage, S3FileStorage>()
            .AddAsyncInitializer<S3FileStorageAsyncInitializer>();
    }
}
