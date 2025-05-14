using System.Data;
using CloudStorage.Domain.Entities;
using CloudStorage.Domain.Entities.Ids;

namespace CloudStorage.Domain.Abstractions;

public interface IFileMetadataDeletedOutboxRepository
{
    Task<EventId> AddAsync(
        FileMetadataOutbox fileMetadataOutbox,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<FileMetadataOutbox>> GetTopUnprocessedOutboxes(
        GetTopUnprocessedOutboxesModel model,
        CancellationToken cancellationToken = default);
}
