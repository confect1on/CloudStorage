using CloudStorage.Domain.Abstractions;

namespace CloudStorage.BackgroundServices;

internal sealed class OutboxPublisherService(
    IUnitOfWorkFactory unitOfWorkFactory) : BackgroundService
{
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            
        }
    }
}
