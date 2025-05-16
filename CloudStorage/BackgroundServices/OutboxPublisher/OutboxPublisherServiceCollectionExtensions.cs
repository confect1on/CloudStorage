namespace CloudStorage.BackgroundServices.OutboxPublisher;

public static class OutboxPublisherServiceCollectionExtensions
{
    public static IServiceCollection AddOutboxPublisher(this IServiceCollection services, IConfiguration configuration) => services
        .Configure<OutboxPublisherSettings>(configuration.GetSection(nameof(OutboxPublisherSettings)))
        .AddHostedService<OutboxPublisherService>();
}
