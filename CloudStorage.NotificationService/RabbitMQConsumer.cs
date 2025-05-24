using System.Text;
using System.Text.Json;
using CloudStorage.NotificationService.Infrastructure.EventBus;
using CloudStorage.NotificationService.Persistence;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CloudStorage.NotificationService;

public class RabbitMQConsumer(
    ILogger<RabbitMQConsumer> logger,
    IConnection rabbitMqConnection,
    IOptions<RabbitMqSettings> rabbitMqSettings,
    IOptions<PersistenceSettings> dbSettings)
    : BackgroundService
{
    private readonly RabbitMqSettings _rabbitMqSettings = rabbitMqSettings.Value;

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var channel = await rabbitMqConnection.CreateChannelAsync(cancellationToken: stoppingToken);
        
        await channel.QueueDeclareAsync(queue: _rabbitMqSettings.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (sender, ea) =>
        {
            await using var connection = new NpgsqlConnection(dbSettings.Value.ConnectionString);
            try
            {
                await connection.OpenAsync(stoppingToken);
                    
                var body = ea.Body.ToArray();

                var filePublishedEvent = JsonSerializer.Deserialize<FilePublishedEventDto>(body)
                    ?? throw new InvalidOperationException("");
                var filePublishedEventOutbox = new FilePublishedInboxDto
                {
                    Id = filePublishedEvent.Id,
                    CreatedAt = filePublishedEvent.CreatedAt,
                    FileMetadataId = filePublishedEvent.FileMetadataId,
                    ReceivedAt = DateTimeOffset.UtcNow, // TODO create provider
                };
                const string sql =
                    """
                    insert into file_published_event_inbox 
                        (id, created_at, file_metadata_id, received_at, processing_at, processed_at, version)
                    values (@Id, @CreatedAt, @FileMetadataId, @ReceivedAt, @ProcessingAt, @ProcessedAt, @Version)
                    on conflict do nothing;
                    """;
                var commandDefinition = new CommandDefinition(
                    sql,
                    filePublishedEventOutbox,
                    cancellationToken: stoppingToken);
                await connection.ExecuteAsync(commandDefinition);

                await channel.BasicAckAsync(ea.DeliveryTag, false, stoppingToken);
                logger.LogInformation("Message processed successfully");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing message");
                await channel.BasicNackAsync(ea.DeliveryTag, false, false, stoppingToken);
            }
        };

        await channel.BasicConsumeAsync(queue: _rabbitMqSettings.QueueName,
            autoAck: false,
            consumer: consumer,
            cancellationToken: stoppingToken);
        await channel.CloseAsync(stoppingToken);
        await channel.DisposeAsync();
        await Task.CompletedTask;
    }

    public async override Task StopAsync(CancellationToken cancellationToken)
    {
        rabbitMqConnection?.CloseAsync(cancellationToken: cancellationToken);
        rabbitMqConnection?.Dispose();
        await base.StopAsync(cancellationToken);
    }
}