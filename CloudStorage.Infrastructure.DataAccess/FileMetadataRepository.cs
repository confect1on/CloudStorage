using CloudStorage.Domain.Abstractions;
using CloudStorage.Domain.Entities;
using CloudStorage.Domain.Entities.Ids;

namespace CloudStorage.Infrastructure.DataAccess;

internal sealed class FileMetadataRepository(DalOptions dalOptions) : PostgresRepository(dalOptions), IFileMetadataRepository
{
    public Task<FileMetadataId> AddAsync(FileMetadata file, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<FileMetadata> GetByIdAsync(FileMetadataId fileMetadataId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task AttachStorageIdAsync(FileMetadataId fileMetadataId, StorageId storageId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteByIdAsync(FileMetadataId fileMetadataId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
