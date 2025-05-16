using System.Text;
using System.Text.Json;
using CloudStorage.Domain;
using CloudStorage.Domain.Abstractions;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace CloudStorage.Infrastructure.EventBus;

internal sealed class RabbitMqEventBus(IOptions<EventBusSettings> options) : IEventBus
{
    public async Task PublishAsync(IDomainEvent message, CancellationToken cancellationToken = default)
    {
        var routingKey = message.GetType().Name;
        var createChannelOptions = new CreateChannelOptions(
            publisherConfirmationsEnabled: true,
            publisherConfirmationTrackingEnabled: false);
        var connectionFactory = new ConnectionFactory
        {
            HostName = options.Value.HostName,
            Port = options.Value.Port,
            UserName = options.Value.UserName,
            Password = options.Value.Password,
        };
        await using var connection = await connectionFactory.CreateConnectionAsync(cancellationToken); 
        await using var channel = await connection.CreateChannelAsync(createChannelOptions, cancellationToken);
        var exchangeName = options.Value.ExchangeName;
        await channel.ExchangeDeclareAsync(exchange: exchangeName, type: "direct", cancellationToken: cancellationToken);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
        await channel.BasicPublishAsync(
            exchange: exchangeName,
            routingKey: routingKey,
            mandatory: true,
            basicProperties: new BasicProperties(),
            body: body,
            cancellationToken: cancellationToken);
    }

    public async Task PublishBatchAsync(IEnumerable<IDomainEvent> events, CancellationToken cancellationToken = default)
    {
        var createChannelOptions = new CreateChannelOptions(
            publisherConfirmationsEnabled: true,
            publisherConfirmationTrackingEnabled: false);
        var connectionFactory = new ConnectionFactory
        {
            HostName = options.Value.HostName,
        };
        await using var connection = await connectionFactory.CreateConnectionAsync(cancellationToken); 
        await using var channel = await connection.CreateChannelAsync(createChannelOptions, cancellationToken);
        var exchangeName = options.Value.ExchangeName;
        
        foreach (var @event in events)
        {
            var routingKey = @event.GetType().Name;
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            await channel.BasicPublishAsync(exchangeName, routingKey, true, new BasicProperties(), body: body, cancellationToken);
        }
    }
}
