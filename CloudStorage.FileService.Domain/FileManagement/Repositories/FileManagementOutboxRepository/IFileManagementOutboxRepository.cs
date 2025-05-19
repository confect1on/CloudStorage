using CloudStorage.FileService.Domain.FileManagement.Entities;
using CloudStorage.FileService.Domain.FileManagement.ValueObjects;

namespace CloudStorage.FileService.Domain.FileManagement.Repositories.FileManagementOutboxRepository;

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