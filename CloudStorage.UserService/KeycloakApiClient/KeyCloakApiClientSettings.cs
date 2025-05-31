namespace UserService.KeycloakApiClient;

public record KeyCloakApiClientSettings
{
    public required string Realm { get; init; }
}
