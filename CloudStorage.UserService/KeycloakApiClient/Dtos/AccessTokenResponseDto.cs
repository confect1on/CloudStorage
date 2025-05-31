namespace UserService.KeycloakApiClient;

public record AccessTokenResponseDto
{
    public required string AccessToken { get; init; }
    
    public int ExpiresIn { get; init; }
}
