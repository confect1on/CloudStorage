using Aspire.Hosting.AWS;

namespace CloudStorage.AppHost;

internal static class DistributedApplicationBuilderExtensions
{
    public static IDistributedApplicationBuilder AddFilesService(
        this IDistributedApplicationBuilder builder,
        IResourceBuilder<PostgresDatabaseResource> filesServiceDb,
        IResourceBuilder<RabbitMQServerResource> rabbitMq,
        IResourceBuilder<MinIOResource> minioS3,
        IAWSSDKConfig awsConfig)
    {
        
        builder
            .AddProject<Projects.CloudStorage_FilesService>("files-service")
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
            ;
        return builder;
    }
}

