namespace CloudStorage.Domain.Entities;

public class FileMetadata
{
    public Guid Id { get; private set; }
    
    public required string Name { get; set; }
    
    
}
