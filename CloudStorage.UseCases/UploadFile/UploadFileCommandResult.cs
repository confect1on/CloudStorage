using CloudStorage.Domain.Entities.Ids;

namespace CloudStorage.UseCases.UploadFile;

public record UploadFileCommandResult(FileMetadataId FileMetadataId);
