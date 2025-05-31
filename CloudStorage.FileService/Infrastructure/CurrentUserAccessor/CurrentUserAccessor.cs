using System.Security.Claims;
using CloudStorage.FileService.Domain.Abstractions;
using CloudStorage.FileService.Domain.UserManagement.ValueObjects;

namespace CloudStorage.FilesService.Infrastructure.CurrentUserAccessor;

internal sealed class CurrentUserAccessor(IHttpContextAccessor httpContextAccessor) : ICurrentUserAccessor
{
    public UserId GetCurrentUserId()
    {
        var subClaimValue = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (subClaimValue is null)
        {
            throw new InvalidOperationException("Cannot access 'sub' claim for the current token.");
        }
        return UserId.Parse(subClaimValue);
    }
}
