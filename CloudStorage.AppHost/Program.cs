var builder = DistributedApplication.CreateBuilder(args);

builder
    .AddProject<Projects.CloudStorage_FilesService>("files-service");

builder.Build().Run();
