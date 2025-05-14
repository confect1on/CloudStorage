using CloudStorage.Domain.Entities.Ids;

namespace CloudStorage.UseCases.GetFile;

public class StorageIsNotAttachedToMetadataException(FileMetadataId fileMetadataId) : Exception($"Storage is not attached to metadata with id: {fileMetadataId}");