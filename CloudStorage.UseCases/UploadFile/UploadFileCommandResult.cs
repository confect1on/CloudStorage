using CloudStorage.Domain.FileManagement.ValueObjects;

namespace CloudStorage.UseCases.UploadFile;

public record UploadFileCommandResult(FileMetadataId FileMetadataId);
