namespace CloudStorage.NotificationService;

public record FilePublishedEventDto
{
    public Guid Id { get; init; }
    
    public DateTimeOffset CreatedAt { get; init; }
    
    public Guid FileMetadataId { get; init; }
}
