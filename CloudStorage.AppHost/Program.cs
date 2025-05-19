using Amazon;
using CloudStorage.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var minioProfile = "local-minio-profile";
var awsProfile = builder
    .AddAWSSDKConfig()
    .WithProfile(minioProfile);
var awsConfig = builder
    .AddAWSSDKConfig()
    .WithProfile(minioProfile)
    .WithRegion(RegionEndpoint.USWest1);

var postgres = builder
    .AddPostgres("postgres")
    .WithPgAdmin();
var filesServiceDb = postgres
    .AddDatabase("files-service-db");

var rabbitMq = builder
    .AddRabbitMQ("rabbitmq")
    .WithManagementPlugin();

var minioS3 = builder
    .AddMinIO("minio")
    // hardcoded cause pommalabs package doesn't support aws integration and alvatec package doesn't work properly
    .WithCredentials("RxPK5N88zPH0R45l9K0O", "Ao5AcPI9xzBu9jUiWevZFcoytDf5DICAw4J0rvJF")
    .WithDataVolume();
builder
    .AddFilesService(filesServiceDb, rabbitMq, minioS3, awsConfig);

builder.Build().Run();
