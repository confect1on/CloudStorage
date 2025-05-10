namespace CloudStorage.UseCases;

public record FileMetadataDto(string FileName, long FileSizeInBytes, string MimeType);
