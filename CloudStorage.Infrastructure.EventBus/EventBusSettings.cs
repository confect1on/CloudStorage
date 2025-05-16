namespace CloudStorage.Infrastructure.EventBus;

public record EventBusSettings
{
    public required string HostName { get; init; }
    
    public required int Port { get; init; }
    
    public required string ExchangeName { get; init; }
    
    public required string UserName { get; init; }
    
    public required string Password { get; init; }
}
