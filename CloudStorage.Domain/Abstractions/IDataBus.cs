namespace CloudStorage.Domain.Abstractions;

public interface IEventBus
{
    Task PublishAsync(
        IDomainEvent message,
        CancellationToken cancellationToken = default);
    
    Task PublishBatchAsync(IEnumerable<IDomainEvent> events, CancellationToken cancellationToken = default);
}
