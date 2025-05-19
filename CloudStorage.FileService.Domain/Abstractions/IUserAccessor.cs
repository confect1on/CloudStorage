using CloudStorage.FileService.Domain.UserManagement.ValueObjects;

namespace CloudStorage.FileService.Domain.Abstractions;

public interface IUserAccessor
{
    UserId GetCurrentUserId();
}
