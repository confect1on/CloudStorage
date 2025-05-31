namespace UserService.KeycloakApiClient;

public record CreateKeyCloakUserCredentialsDto
{
    public required string Type { get; init; }
    
    public required string Value { get; init; }
    
    public bool Temporary { get; init; }
}
