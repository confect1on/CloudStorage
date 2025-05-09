using CloudStorage.Domain.Entities.Ids;

namespace CloudStorage.Domain.Exceptions;

public class FileMetadataNotFoundException(FileMetadataId fileMetadataId) : Exception($"File with id {fileMetadataId} not found");
