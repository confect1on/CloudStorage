using CloudStorage.NotificationService.Domain.NotificationManagement.UserApiClient;

namespace CloudStorage.NotificationService.Infrastructure.UserApiClient;

internal sealed class MockUserApiClient : IUserApiClient
{
    public Task<UserDto> GetUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
