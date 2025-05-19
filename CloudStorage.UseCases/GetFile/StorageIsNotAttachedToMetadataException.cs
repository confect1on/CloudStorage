using CloudStorage.FileService.Domain.FileManagement.ValueObjects;

namespace CloudStorage.UseCases.GetFile;

public class StorageIsNotAttachedToMetadataException(FileMetadataId fileMetadataId) : Exception($"Storage is not attached to metadata with id: {fileMetadataId}");