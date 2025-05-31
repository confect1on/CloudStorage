namespace UserService.Services;

public interface IUserService
{
    public Task<Guid> CreateUserAsync(CreateUserDto createUserDto, CancellationToken cancellationToken = default);
}
