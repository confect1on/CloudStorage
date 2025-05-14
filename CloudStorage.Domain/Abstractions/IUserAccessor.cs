
using CloudStorage.Domain.UserManagement.ValueObjects;

namespace CloudStorage.Domain.Abstractions;

public interface IUserAccessor
{
    UserId GetCurrentUserId();
}
