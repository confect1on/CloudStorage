using UserService.Services;

namespace UserService.KeycloakApiClient;

public interface IKeycloakApiClient
{
    public Task<Guid> CreateUserAsync(CreateUserDto createUserDto, CancellationToken cancellationToken = default);
}
