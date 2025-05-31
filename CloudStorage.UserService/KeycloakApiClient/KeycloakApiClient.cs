using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RestSharp;
using UserService.Services;

namespace UserService.KeycloakApiClient;

internal sealed class KeycloakApiClient(
    HttpClient httpClient,
    IOptions<KeyCloakApiClientSettings> options) : IKeycloakApiClient
{
    public async Task<Guid> CreateUserAsync(CreateUserDto createUserDto, CancellationToken cancellationToken = default)
    {
        // TODO: get and refresh token via polly
        var token = await GetAccessTokenAsync(cancellationToken);
        var message = new HttpRequestMessage(HttpMethod.Post, $"/realms/{options.Value.Realm}/api/users");
        message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var dto = new CreateKeyCloakUserRequestDto()
        {
            Email = createUserDto.Email,
            Enabled = true,
            Username = createUserDto.Username,
            Credentials = new CreateKeyCloakUserCredentialsDto()
            {
                Temporary = false,
                Type = "password",
                Value = createUserDto.Password,
            },
        };
        message.Content = new StringContent(JsonSerializer.Serialize(dto));
        var response = await httpClient.SendAsync(message, cancellationToken);
        response.EnsureSuccessStatusCode();
        var id = response.Headers.Location?.Segments.LastOrDefault();
        if (string.IsNullOrEmpty(id))
        {
            throw new InvalidOperationException("Missing \"Location\" header");
        }
        return Guid.Parse(id);

    }

    public async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        var address = $"/realms/{options.Value.Realm}/protocol/openid-connect/token";
        var clientId = "admin-api-client";
        var clientSecret = "H3cdyDInHrjAO9VXMsyUZvpA5a0HMb9U";
        var authenticationString = $"{clientId}:{clientSecret}";
        var encodedAuthenticationString = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(authenticationString));
        var values = new List<KeyValuePair<string, string>>
        {
            new("grant_type", "client_credentials"),
        };
        var content = new FormUrlEncodedContent(values);
        var message = new HttpRequestMessage(HttpMethod.Post, address);
        message.Headers.Authorization = new AuthenticationHeaderValue("Basic", encodedAuthenticationString);
        message.Content = content;
        var response = await httpClient.SendAsync(message, cancellationToken);
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync(cancellationToken);
        var responseDto = JsonSerializer.Deserialize<AccessTokenResponseDto>(body) ?? throw new InvalidOperationException("Response cannot be null");
        return responseDto.AccessToken;
    }
}
