namespace CloudStorage.NotificationService.Domain.NotificationManagement;

public record UserDto
{
    public Guid Id { get; init; }

    public required string Email { get; init; }
}
