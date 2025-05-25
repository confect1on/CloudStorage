using CloudStorage.FileService.Domain.Abstractions;
using CloudStorage.FileService.Domain.UserManagement.ValueObjects;

namespace CloudStorage.FilesService.Infrastructure.CurrentUserAccessor;

internal sealed class CurrentUserAccessor(IHttpContextAccessor httpContextAccessor) : ICurrentUserAccessor
{
    public UserId GetCurrentUserId()
    {
        var subClaimValue = httpContextAccessor.HttpContext?.User.FindFirst("sub")?.Value;
        if (subClaimValue is null)
        {
            throw new InvalidOperationException("Cannot access 'sub' claim for current user.");
        }
        return UserId.Parse(subClaimValue);
    }
}
