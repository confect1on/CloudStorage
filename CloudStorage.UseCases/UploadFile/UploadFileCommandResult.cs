using CloudStorage.FileService.Domain.FileManagement.ValueObjects;

namespace CloudStorage.UseCases.UploadFile;

public record UploadFileCommandResult(FileMetadataId FileMetadataId);
