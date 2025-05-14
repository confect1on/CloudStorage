using CloudStorage.Domain.FileManagement.Entities;
using CloudStorage.Domain.FileManagement.ValueObjects;

namespace CloudStorage.Domain.FileManagement;

public interface IFileMetadataRepository
{
    Task<FileMetadataId> AddAsync(FileMetadata fileMetadata, CancellationToken cancellationToken = default);
    
    Task<FileMetadata> GetByIdAsync(FileMetadataId fileMetadataId, CancellationToken cancellationToken = default);
    
    Task AttachStorageIdAsync(FileMetadataId fileMetadataId, StorageId? storageId, CancellationToken cancellationToken = default);
    
    Task DeleteByIdAsync(
        FileMetadataId fileMetadataId,
        CancellationToken cancellationToken = default);
}
