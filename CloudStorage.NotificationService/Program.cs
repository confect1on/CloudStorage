using CloudStorage.NotificationService;
using CloudStorage.NotificationService.BLL.Notifications;
using CloudStorage.NotificationService.Infrastructure;
using CloudStorage.NotificationService.Infrastructure.EventBus;
using CloudStorage.NotificationService.Infrastructure.Persistence;
using CloudStorage.NotificationService.Infrastructure.Persistence.Extensions;
using CloudStorage.NotificationService.Persistence;
using CloudStorage.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);
builder.AddServiceDefaults();
builder.AddEventBus();
// builder.Services
//     .AddHostedService<RabbitMQConsumer>()
//     .AddHostedService<InboxMessageProcessor>();
builder.Services
    .AddPersistenceServices(builder.Configuration)
    .AddApiClients();

builder.Services
    .AddNotificationServices(builder.Configuration);
var host = builder.Build();
host.MigrateUp();
host.Run();
