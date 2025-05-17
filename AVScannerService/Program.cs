using AVScannerService;
using AVScannerService.Persistence;
using AVScannerService.Persistence.Extensions;

var builder = Host.CreateApplicationBuilder(args);
builder.Services
    .ConfigurePersistenceServices(builder.Configuration)
    .AddPersistenceServices()
    .AddMigrations();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.MigrateUp();
host.Run();
