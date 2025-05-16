using CloudStorage.Domain.FileManagement.Entities;
using CloudStorage.Domain.FileManagement.ValueObjects;

namespace CloudStorage.Domain.FileManagement.Repositories.FileManagementOutboxRepository;

public interface IFileManagementOutboxRepository
{
    Task<FileManagementOutboxId> AddAsync(
        FileManagementOutbox fileManagementOutbox,
        CancellationToken cancellationToken = default);

    Task<FileManagementOutboxId> AddAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<FileManagementOutbox>> GetTopUnprocessedAsync(
        GetTopUnprocessedOutboxesModel model,
        CancellationToken cancellationToken = default);
    
    Task SetErrorMessageAsync(
        SetErrorMessageModel model,
        CancellationToken cancellationToken = default);
    
    Task MarkProcessedAsync(
        MarkProcessedModel model,
        CancellationToken cancellationToken = default);
}