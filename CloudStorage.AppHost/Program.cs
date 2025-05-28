using Amazon;
using CloudStorage.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var minioProfile = "local-minio-profile";
var awsConfig = builder
    .AddAWSSDKConfig()
    .WithProfile(minioProfile)
    .WithRegion(RegionEndpoint.USWest1);

var postgres = builder.AddPostgres();
var rabbitMq = builder.AddRabbitMQ();
var minioS3 = builder.AddMinioS3();
var keycloak = builder.AddKeycloak();

builder
    .AddFilesService(postgres, rabbitMq, minioS3, keycloak, awsConfig);

var notificationServiceDb = postgres
    .AddDatabase("notifications-service-db");
builder
    .AddProject<Projects.CloudStorage_NotificationService>("notification-service")
    .WithEnvironment("PersistenceSettings__ConnectionString", notificationServiceDb)
    .WaitFor(notificationServiceDb)
    .WithEnvironment("RabbitMQSettings__ConnectionString", rabbitMq)
    .WaitFor(rabbitMq);

builder
    .AddProject<Projects.AuthService>("auth-service")
    .WithReference(keycloak)
    .WaitFor(keycloak);
builder.Build().Run();
