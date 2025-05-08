namespace CloudStorage.Domain;

public class FileMetadata
{
    public Guid Id { get; private set; }
    
    public required string Name { get; set; }
    
    
}
