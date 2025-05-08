namespace CloudStorage.UseCases.UploadFile;

public record FileMetadataDto(string FileName, long FileSizeInBytes, string MimeType);
