using Extensions.Hosting.AsyncInitialization;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace CloudStorage.Infrastructure.EventBus;

internal sealed class EventBusAsyncInitializer(IOptions<EventBusSettings> options) : IAsyncInitializer
{
    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        var connectionFactory = new ConnectionFactory
        {
            Uri = new Uri(options.Value.ConnectionString),
        };
        await using var connection = await connectionFactory.CreateConnectionAsync(cancellationToken);
        await using var channel = await connection.CreateChannelAsync(
            new CreateChannelOptions(true, false),
            cancellationToken);
        await channel.ExchangeDeclareAsync(options.Value.ExchangeName, ExchangeType.Direct, cancellationToken: cancellationToken);
    }
}
