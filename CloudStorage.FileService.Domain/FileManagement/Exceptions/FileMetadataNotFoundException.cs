using CloudStorage.FileService.Domain.FileManagement.ValueObjects;

namespace CloudStorage.FileService.Domain.FileManagement.Exceptions;

public class FileMetadataNotFoundException(FileMetadataId fileMetadataId) : Exception($"File with id {fileMetadataId} not found");
