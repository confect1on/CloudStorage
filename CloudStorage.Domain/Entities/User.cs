using CloudStorage.Domain.Entities.Ids;

namespace CloudStorage.Domain.Entities;

public class User
{
    public long Id { get; set; }
    
    public required string Email { get; set; }
    
    public required string NormalizedEmail { get; set; }
    
    public required string PasswordHash { get; set; }
}
