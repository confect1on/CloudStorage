namespace CloudStorage.UseCases.GetFile;

public record GetFileQueryResult(Stream FileStream, FileMetadataDto FileMetadataDto);
