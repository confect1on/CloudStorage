using CloudStorage.Domain.Entities.Ids;

namespace CloudStorage.Domain.Abstractions;

public interface IUserAccessor
{
    UserId GetCurrentUserId();
}
