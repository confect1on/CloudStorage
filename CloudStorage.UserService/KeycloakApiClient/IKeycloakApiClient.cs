using UserService.Services;

namespace UserService.KeycloakApiClient;

public interface IKeycloakApiClient
{
    public Task CreateUserAsync(CreateUserDto createUserDto, CancellationToken cancellationToken = default);
}
