using CloudStorage.FileService.Domain.Abstractions;
using CloudStorage.FileService.Domain.UserManagement.ValueObjects;

namespace CloudStorage.FileService.Domain.DomainServices;

internal sealed class UserAccessor : IUserAccessor
{
    public UserId GetCurrentUserId()
    {
        return new UserId();
    }
}
