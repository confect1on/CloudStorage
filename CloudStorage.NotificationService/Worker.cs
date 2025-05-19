using System.Text;
using CloudStorage.NotificationService.Persistence;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CloudStorage.NotificationService;

public class Worker(
    ILogger<Worker> logger,
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
                var message = Encoding.UTF8.GetString(body);
                logger.LogInformation("Received message: {message}", message);

                var inboxMessage = new InboxMessage
                {
                    Id = Guid.NewGuid(),
                    Body = message,
                    ReceivedAt = DateTime.UtcNow
                };

                const string sql =
                    """
                    INSERT INTO inbox_messages (id, body, received_at)
                    VALUES (@Id, @Body, @ReceivedAt)
                    """;
                    
                await connection.ExecuteAsync(sql, inboxMessage);

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

// Настройки базы данных

// Настройки RabbitMQ
public class RabbitMqSettings
{
    public string QueueName { get; set; }
}

// Модель для таблицы inbox
public class InboxMessage
{
    public Guid Id { get; set; }
    public string Body { get; set; }
    public DateTime ReceivedAt { get; set; }
}