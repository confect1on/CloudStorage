using CloudStorage.Domain.FileManagement.ValueObjects;

namespace CloudStorage.Domain.FileManagement.Exceptions;

public class FileMetadataNotFoundException(FileMetadataId fileMetadataId) : Exception($"File with id {fileMetadataId} not found");
