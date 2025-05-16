using CloudStorage.Domain.Abstractions;
using CloudStorage.Domain.FileManagement;
using CloudStorage.Domain.FileManagement.Repositories.FileManagementOutboxRepository;
using CloudStorage.Domain.FileManagement.ValueObjects;
using CloudStorage.Domain.UserManagement.ValueObjects;
using CloudStorage.Infrastructure.Persistence.ConnectionFactory;
using CloudStorage.Infrastructure.Persistence.Persistence.Migrations;
using CloudStorage.Infrastructure.Persistence.Repositories.FileManagementOutbox;
using CloudStorage.Infrastructure.Persistence.Repositories.FileMetadata;
using Dapper;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CloudStorage.Infrastructure.Persistence.Extensions;

public static class PersistenceServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        SqlMapper.AddTypeHandler(new FileMetadataId.DapperTypeHandler());
        SqlMapper.AddTypeHandler(new StorageId.DapperTypeHandler());
        SqlMapper.AddTypeHandler(new UserId.DapperTypeHandler());
        SqlMapper.AddTypeHandler(new FileManagementOutboxId.DapperTypeHandler());
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        return services
            .ConfigurePersistence(configuration)
            .AddMigrations()
            .AddConnectionFactory()
            .AddRepositories()
            .AddUnitOfWorks();
    }

    internal static IServiceCollection AddConnectionFactory(this IServiceCollection serviceCollection) => serviceCollection
        .AddSingleton<IConnectionFactory, ConnectionFactory.ConnectionFactory>();
    
    internal static IServiceCollection AddRepositories(this IServiceCollection services) => services
        .AddSingleton<IRepositoryFactory<IFileMetadataRepository>, FileMetadataRepositoryFactory>()
        .AddScoped<IFileMetadataRepository>(x => x
            .GetRequiredService<IRepositoryFactory<IFileMetadataRepository>>()
            .Create())
        .AddSingleton<IRepositoryFactory<IFileManagementOutboxRepository>, FileManagementOutboxRepositoryFactory>()
        .AddScoped<IFileManagementOutboxRepository>(x => x
            .GetRequiredService<IRepositoryFactory<IFileManagementOutboxRepository>>()
            .Create());

    internal static IServiceCollection AddUnitOfWorks(this IServiceCollection services) => services
        .AddSingleton<IUnitOfWorkFactory, DapperUnitOfWorkFactory>()
        .AddScoped<IUnitOfWork, DapperUnitOfWork>();
    
    internal static IServiceCollection AddMigrations(this IServiceCollection services) => services.AddFluentMigratorCore()
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

    internal static IServiceCollection ConfigurePersistence(this IServiceCollection services, IConfiguration configuration) => services
        .Configure<DalSettings>(configuration.GetSection(nameof(DalSettings)));
}
