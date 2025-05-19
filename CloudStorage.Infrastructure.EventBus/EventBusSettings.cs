namespace CloudStorage.Infrastructure.EventBus;

public record EventBusSettings
{
    public required string ExchangeName { get; init; }
    
    public required string ConnectionString { get; init; }
}
