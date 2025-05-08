using CloudStorage.Domain.Entities;
using CloudStorage.Domain.Entities.Ids;

namespace CloudStorage.Domain.Abstractions;

public interface IFileMetadataRepository
{
    Task<FileMetadataId> AddAsync(FileMetadata file, CancellationToken cancellationToken = default);
    
    Task<FileMetadata> GetByIdAsync(FileMetadataId fileMetadataId, CancellationToken cancellationToken = default);
    
    Task AttachStorageIdAsync(FileMetadataId fileMetadataId, StorageId storageId, CancellationToken cancellationToken = default);
}
