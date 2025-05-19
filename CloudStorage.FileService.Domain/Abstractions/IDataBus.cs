namespace CloudStorage.FileService.Domain.Abstractions;

public interface IEventBus
{
    Task PublishAsync(
        IDomainEvent @event,
        CancellationToken cancellationToken = default);
}
