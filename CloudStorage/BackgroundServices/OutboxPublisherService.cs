using System.Data;
using System.Reflection;
using System.Text.Json;
using CloudStorage.Domain;
using CloudStorage.Domain.Abstractions;
using CloudStorage.Domain.FileManagement.Repositories.FileManagementOutboxRepository;

namespace CloudStorage.BackgroundServices;

internal sealed class OutboxPublisherService(
    IUnitOfWorkFactory unitOfWorkFactory,
    IEventBus eventBus) : BackgroundService
{
    private readonly Assembly _domainEventsAssembly = typeof(IDomainEvent).Assembly;
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();
        while (!stoppingToken.IsCancellationRequested)
        {
            using var unitOfWork = unitOfWorkFactory.Create();
            var model = new GetTopUnprocessedOutboxesModel(100);
            await unitOfWork.BeginTransactionAsync(IsolationLevel.RepeatableRead, stoppingToken);
            var outboxes = await unitOfWork.FileManagementOutboxRepository
                .GetTopUnprocessedAsync(model, stoppingToken);
            foreach (var outbox in outboxes)
            {
                try
                {
                    var outboxType = _domainEventsAssembly.GetType(outbox.Type) ?? throw new Exception($"Could not load outbox type: {outbox.Type}");
                    var deserializedMessage = JsonSerializer.Deserialize(outbox.Content, outboxType) as IDomainEvent
                        ?? throw new Exception($"Could not deserialize domain event: {outbox.Type}");
                    await eventBus.PublishAsync(deserializedMessage, stoppingToken);
                    await unitOfWork.FileManagementOutboxRepository.MarkProcessedAsync(new MarkProcessedModel(outbox.Id), stoppingToken);
                }
                catch (Exception ex)
                {
                    await unitOfWork.FileManagementOutboxRepository
                        .SetErrorMessageAsync(new SetErrorMessageModel(outbox.Id, ex.Message), stoppingToken);
                }
                finally
                {
                    await unitOfWork.CommitAsync(stoppingToken);
                }
            }
        }
    }
}
