namespace UserService.Services;

public record CreateUserDto
{
    public required string Email { get; init; }
    
    public required string Password { get; init; }
    
    public required string Username { get; init; }
};
