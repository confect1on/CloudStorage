namespace CloudStorage.NotificationService.Persistence.Repositories;

internal sealed class FilePublishedInboxDto
{
    public Guid Id { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public Guid FileMetadataId { get; set; }
    
    public DateTimeOffset ReceivedAt { get; set; }
    
    public DateTimeOffset? ProcessingAt { get; set; }
    
    public DateTimeOffset ProcessedAt { get; set; }
    
    public long Version { get; set; }
}
