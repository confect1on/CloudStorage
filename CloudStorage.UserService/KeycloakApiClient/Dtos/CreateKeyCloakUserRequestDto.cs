namespace UserService.KeycloakApiClient;

public record CreateKeyCloakUserRequestDto
{
    public required string Username { get; init; }
    
    public required string Email { get; init; }
    
    public bool Enabled { get; init; }
    
    public required CreateKeyCloakUserCredentialsDto Credentials { get; init; }
}
