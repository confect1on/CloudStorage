using CloudStorage.Domain.FileManagement.Entities;

namespace CloudStorage.UseCases.Mappers;

public static class FileMetadataDtoMapper
{
    public static FileMetadataDto MapToFileMetadataDto(this FileMetadata fileMetadata) => new(fileMetadata.FileName, fileMetadata.FileSizeInBytes, fileMetadata.MimeType);
}
