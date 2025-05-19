using CloudStorage.NotificationService;
using CloudStorage.NotificationService.Persistence;
using CloudStorage.NotificationService.Persistence.Extensions;
using CloudStorage.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);
builder.AddServiceDefaults();
// builder.AddRabbitMQClient();
// builder.Services.AddHostedService<Worker>();
builder.Services.AddPersistenceServices(builder.Configuration);
var host = builder.Build();
host.MigrateUp();
host.Run();
