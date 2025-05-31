using RestSharp;
using UserService.Services;

namespace UserService.KeycloakApiClient;

internal sealed class KeycloakApiClient(RestClient restClient) : IKeycloakApiClient
{
    public Task CreateUserAsync(CreateUserDto createUserDto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
