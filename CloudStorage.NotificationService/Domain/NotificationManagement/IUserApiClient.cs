namespace CloudStorage.NotificationService.Domain.NotificationManagement;

public interface IUserApiClient
{
    Task<UserDto> GetUserAsync(Guid userId, CancellationToken cancellationToken = default);
}
