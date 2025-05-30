﻿using System.Text;
using System.Text.Json;
using CloudStorage.FileService.Domain;
using CloudStorage.FileService.Domain.Abstractions;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace CloudStorage.Infrastructure.EventBus;

internal sealed class RabbitMqEventBus(IOptions<EventBusSettings> options) : IEventBus
{
    public async Task PublishAsync(IDomainEvent @event, CancellationToken cancellationToken = default)
    {
        var createChannelOptions = new CreateChannelOptions(
            publisherConfirmationsEnabled: true,
            publisherConfirmationTrackingEnabled: false);
        var connectionFactory = new ConnectionFactory
        {
            Uri = new Uri(options.Value.ConnectionString),
        };
        await using var connection = await connectionFactory.CreateConnectionAsync(cancellationToken); 
        await using var channel = await connection.CreateChannelAsync(createChannelOptions, cancellationToken);
        await channel.QueueDeclareAsync(
            queue: @event.EventGroup,
            durable: true,
            exclusive: false,
            autoDelete: false,
            cancellationToken: cancellationToken);
        var exchangeName = options.Value.ExchangeName;
        await channel.QueueBindAsync(
            queue: @event.EventGroup,
            exchange: exchangeName,
            routingKey: @event.Key,
            cancellationToken: cancellationToken
            );
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
        await channel.BasicPublishAsync(
            exchange: exchangeName,
            routingKey: @event.Key,
            mandatory: true,
            basicProperties: new BasicProperties(),
            body: body,
            cancellationToken: cancellationToken);
    }
}
