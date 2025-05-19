using CloudStorage.FileService.Domain.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CloudStorage.Infrastructure.EventBus;

public static class EventBusServiceCollectionExtensions
{
    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration) => services
        .Configure<EventBusSettings>(configuration.GetSection(nameof(EventBusSettings)))
        .AddAsyncInitializer<EventBusAsyncInitializer>()
        .AddSingleton<IEventBus, RabbitMqEventBus>();
}
