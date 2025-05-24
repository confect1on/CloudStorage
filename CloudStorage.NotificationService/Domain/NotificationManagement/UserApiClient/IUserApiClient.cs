namespace CloudStorage.NotificationService.Domain.NotificationManagement.UserApiClient;

public interface IUserApiClient
{
    Task<UserDto> GetUserAsync(Guid userId, CancellationToken cancellationToken = default);
}
