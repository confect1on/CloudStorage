namespace CloudStorage.NotificationService.Domain.NotificationManagement.UserApiClient;

public record UserDto
{
    public Guid Id { get; init; }

    public required string Email { get; init; }
    
    public required string UserName { get; init; }
}
