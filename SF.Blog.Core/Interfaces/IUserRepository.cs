namespace SF.Blog.Core;
/// <summary>
/// This interface is for facade pattern to separate domain aggregates with framework based entities for authorization and authentication.
/// </summary>
public interface IUserWriteRepository
{
	Task<User> AddAsync(User user);
	Task<User> UpdateAsync(User user);
	Task<User> AddToRoleAsync(User user, string role);
	Task<User> RemoveFromRoleAsync(User user, Role role);
	Task DeleteAsync(User user);
}
