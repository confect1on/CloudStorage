using CloudStorage.Domain.Abstractions;
using CloudStorage.Domain.Entities.Ids;

namespace CloudStorage.Domain.DomainServices;

internal sealed class UserAccessor : IUserAccessor
{
    public UserId GetCurrentUserId()
    {
        return new UserId(1);
    }
}
