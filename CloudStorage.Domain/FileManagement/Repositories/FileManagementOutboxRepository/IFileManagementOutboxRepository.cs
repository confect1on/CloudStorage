using CloudStorage.Domain.FileManagement.Entities;
using CloudStorage.Domain.FileManagement.ValueObjects;

namespace CloudStorage.Domain.FileManagement.Repositories.FileManagementOutboxRepository;

public interface IFileManagementOutboxRepository
{
    Task<FileManagementOutboxId> AddAsync(
        FileManagementOutbox fileManagementOutbox,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<FileManagementOutbox>> GetTopUnprocessedOutboxes(
        GetTopUnprocessedOutboxesModel model,
        CancellationToken cancellationToken = default);
}
