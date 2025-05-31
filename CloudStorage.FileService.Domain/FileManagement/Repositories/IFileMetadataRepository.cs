using CloudStorage.FileService.Domain.FileManagement.Entities;
using CloudStorage.FileService.Domain.FileManagement.ValueObjects;

namespace CloudStorage.FileService.Domain.FileManagement.Repositories;

public interface IFileMetadataRepository
{
    Task<FileMetadataId> AddAsync(FileMetadata fileMetadata, CancellationToken cancellationToken = default);
    
    Task<FileMetadata> GetByIdAsync(FileMetadataId fileMetadataId, CancellationToken cancellationToken = default);
    
    Task AttachStorageIdAsync(FileMetadataId fileMetadataId, StorageId? storageId, CancellationToken cancellationToken = default);
    
    Task DeleteByIdAsync(
        FileMetadataId fileMetadataId,
        CancellationToken cancellationToken = default);
}
