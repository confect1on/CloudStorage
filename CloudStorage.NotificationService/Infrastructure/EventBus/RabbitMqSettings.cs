namespace CloudStorage.NotificationService.Infrastructure.EventBus;

public class RabbitMqSettings
{
    public required string QueueName { get; set; }
    
    public required string ConnectionString { get; set; }
}
