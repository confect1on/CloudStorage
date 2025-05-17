namespace AVScannerService;

public class FileCreatedEventDto
{
    public required Guid Id { get; init; }
    
    public Guid AggregateId { get; init; }
    
    public DateTimeOffset CreatedAt { get; init; }
    
    public required string TemporaryStorageId { get; init; }
}
