using UserService.KeycloakApiClient;

namespace UserService.Services;

internal sealed class UserService(IKeycloakApiClient keycloakApiClient) : IUserService
{
    public Task<Guid> CreateUserAsync(CreateUserDto createUserDto, CancellationToken cancellationToken = default)
    {
        // TODO: send an event to event bus
        return keycloakApiClient.CreateUserAsync(createUserDto, cancellationToken);
    }
}
