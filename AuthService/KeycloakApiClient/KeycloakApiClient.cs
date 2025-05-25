using AuthService.Services;
using RestSharp;

namespace AuthService.KeycloakApiClient;

internal sealed class KeycloakApiClient(RestClient restClient) : IKeycloakApiClient
{
    public Task CreateUserAsync(CreateUserDto createUserDto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
