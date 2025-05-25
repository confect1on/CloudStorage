using AuthService.Services;

namespace AuthService.KeycloakApiClient;

public interface IKeycloakApiClient
{
    public Task CreateUserAsync(CreateUserDto createUserDto, CancellationToken cancellationToken = default);
}
