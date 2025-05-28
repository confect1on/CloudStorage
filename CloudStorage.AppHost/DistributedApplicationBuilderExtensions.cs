using Aspire.Hosting.AWS;

namespace CloudStorage.AppHost;

internal static class DistributedApplicationBuilderExtensions
{
    public static IDistributedApplicationBuilder AddFilesService(
        this IDistributedApplicationBuilder builder,
        IResourceBuilder<PostgresServerResource> postgres,
        IResourceBuilder<RabbitMQServerResource> rabbitMq,
        IResourceBuilder<MinIOResource> minioS3,
        IResourceBuilder<KeycloakResource> keycloak,
        IAWSSDKConfig awsConfig)
    {
        var filesServiceDb = postgres
            .AddDatabase("files-service-db");
        builder
            .AddProject<Projects.CloudStorage_FileService>("files-service")
            .WithEnvironment("OutboxPublisherService__BatchSize", "10")
            .WithReference(filesServiceDb)
            .WithEnvironment("DalSettings__PostgresConnectionString", filesServiceDb)
            .WaitFor(filesServiceDb)
            .WithReference(rabbitMq)
            .WithEnvironment("EventBusSettings__ConnectionString", rabbitMq)
            .WithEnvironment("EventBusSettings__ExchangeName", "test-exchange")
            .WaitFor(rabbitMq)
            .WithReference(minioS3)
            .WithEnvironment("AWS__ServiceURL", "http://localhost:9000/api")
            .WithEnvironment("S3FileStorageSettings__S3Bucket", "dev-files-bucket")
            .WithEnvironment("S3FileStorageSettings__TemporaryS3Bucket", "temporary-bucket")
            .WaitFor(minioS3)
            .WithReference(awsConfig)
            .WithReference(keycloak)
            .WaitFor(keycloak);
        return builder;
    }
    
    public static IResourceBuilder<MinIOResource> AddMinioS3(this IDistributedApplicationBuilder builder) => builder
        .AddMinIO("minio", apiPort: 9000)
        // hardcoded cause pommalabs package doesn't support aws integration and alvatec package doesn't work properly
        .WithCredentials("RxPK5N88zPH0R45l9K0O", "Ao5AcPI9xzBu9jUiWevZFcoytDf5DICAw4J0rvJF")
        .WithDataVolume();
    
    public static IResourceBuilder<RabbitMQServerResource> AddRabbitMQ(this IDistributedApplicationBuilder builder) => builder
        .AddRabbitMQ("rabbitmq")
        .WithManagementPlugin()
        .WithDataVolume();
    
    public static IResourceBuilder<PostgresServerResource> AddPostgres(this IDistributedApplicationBuilder builder) => builder
        .AddPostgres("postgres", port: 15432)
        .WithPgAdmin()
        .WithDataVolume();

    public static IResourceBuilder<KeycloakResource> AddKeycloak(this IDistributedApplicationBuilder builder) => builder
        .AddKeycloak("keycloak", 8080)
        .WithDataVolume();
}

