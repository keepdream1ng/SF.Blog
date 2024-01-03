namespace SF.Blog.Core;
/// <summary>
/// This interface is for facade pattern to separate domain aggregates with framework based entities for authorization and authentication.
/// </summary>
public interface IUserRepository
{
	Task<User?> GetByIdAsync(string Id);
	Task UpdateAsync(User entity);
	Task DeleteAsync(User entity);
	Task AddToRoleAsync(User user, string role);
	Task RemoveFromRoleAsync(User user, Role role);
}
