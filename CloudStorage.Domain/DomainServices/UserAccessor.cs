using CloudStorage.Domain.Abstractions;
using UserId = CloudStorage.Domain.UserManagement.ValueObjects.UserId;

namespace CloudStorage.Domain.DomainServices;

internal sealed class UserAccessor : IUserAccessor
{
    public UserId GetCurrentUserId()
    {
        return new UserId(1);
    }
}
