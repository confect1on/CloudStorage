using System.Text;
using System.Text.Json;
using AVScannerService.Persistence.Abstractions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AVScannerService;

public class Worker(ILogger<Worker> logger, IFileCreatedEventInboxRepository fileCreatedEventInboxRepository) : BackgroundService
{
    private readonly IFileCreatedEventInboxRepository _fileCreatedEventInboxRepository = fileCreatedEventInboxRepository;

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            Port = 5672,
            UserName = "rmuser",
            Password = "rmpassword"
        };
        await using var connection = await factory.CreateConnectionAsync(stoppingToken);
        await using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);
        const string queueName = "files.created";
        await channel.QueueDeclareAsync(
            queue: "files.created",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: stoppingToken);

        await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false, cancellationToken: stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (sender, ea) =>
        {
            var consumerChannel = (sender as AsyncEventingBasicConsumer)?.Channel
                ?? throw new InvalidOperationException("Cannot get access to the channel");
            var bodyCopy = ea.Body.ToArray();
            var inboxDto = JsonSerializer.Deserialize<FileCreatedEventDto>(bodyCopy);
            if (inboxDto == null)
            {
                await consumerChannel.BasicNackAsync(
                    deliveryTag: ea.DeliveryTag,
                    multiple: false,
                    requeue: false,
                    cancellationToken: stoppingToken);
                return;
            }
            
            await _fileCreatedEventInboxRepository.InsertAsync(inboxDto, stoppingToken);

            logger.LogDebug("Received event with id {EventId}", inboxDto.Id);
            await consumerChannel.BasicAckAsync(
                deliveryTag: ea.DeliveryTag,
                multiple: false,
                cancellationToken: stoppingToken);
        };
        
        await channel.BasicConsumeAsync(
            queue: queueName,
            autoAck: false,
            consumer: consumer,
            cancellationToken: stoppingToken);
    }
}
