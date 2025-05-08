using CloudStorage.Domain.Entities.Ids;

namespace CloudStorage.Domain.Exceptions;

public class FileNotFoundException(FileMetadataId fileMetadataId) : Exception($"File with id {fileMetadataId} not found");
