using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Services;

namespace UserService.Controllers;

[ApiController]
[Route("[controller]")]
public class PublicUserController(IUserService userService) : ControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    public Task<Guid> Register(
        [FromBody] CreateUserDto createUserDto,
        CancellationToken cancellationToken)
    {
        return userService.CreateUserAsync(createUserDto, cancellationToken);
    }
}
