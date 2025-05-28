using CloudStorage.FileService.Domain.FileManagement.ValueObjects;
using CloudStorage.FileService.Domain.UserManagement.ValueObjects;

namespace CloudStorage.FileService.Domain.FileManagement.Entities;

public class FileMetadata
{
    public FileMetadataId Id { get; set; }
    
    public required UserId OwnerUserId { get; set; }
    
    public StorageId? StorageId { get; set; }
    
    public required string FileName { get; set; }

    public required long FileSizeInBytes { get; set; }
    
    public required string MimeType { get; set; }

    public required DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    
    public DateTimeOffset? DeletedAt { get; set; }
}
