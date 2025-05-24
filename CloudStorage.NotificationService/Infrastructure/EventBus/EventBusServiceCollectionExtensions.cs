namespace CloudStorage.NotificationService.Infrastructure.EventBus;

internal static class EventBusHostApplicationBuilder
{
    public static IHostApplicationBuilder AddEventBus(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection(nameof(RabbitMqSettings)));
        
        builder.AddRabbitMQClient("RabbitMQ",
            settings =>
            {
                settings.ConnectionString = builder.Configuration.GetSection(nameof(RabbitMqSettings))[nameof(RabbitMqSettings.ConnectionString)];
            });
        return builder;
    }
}
