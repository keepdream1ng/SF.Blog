namespace SF.Blog.Core;
/// <summary>
/// Manager class, what exposes to public internal <see cref="User"/> aggregate methods. Can be get after domain level access check via <see cref="AuthForManagerService"/>.
/// </summary>
public class UserManager
{
	public User ManagedUser { get; private set; }
	private readonly IUserWriteRepository _userRepo;

	// Constructor is internal so only domain services can create manager after checking user access.
    internal UserManager(User user, IUserWriteRepository userRepo)
    {
		ManagedUser = user;
		_userRepo = userRepo;
	}

	public async Task<User> UpdateAsync(string name, string about, DateTime dateOfBirth)
	{
		ManagedUser.Update(name, about, dateOfBirth);
		return await _userRepo.UpdateAsync(ManagedUser);
	}

	public async Task<User> AddRoleAsync(string roleName)
	{
		ManagedUser.AddRole(roleName);
		return await _userRepo.AddToRoleAsync(ManagedUser, roleName);
	}
	public async Task<User> RemoveRoleAsync(Role role)
	{
		ManagedUser.RemoveRole(role);
		return await _userRepo.RemoveFromRoleAsync(ManagedUser, role);
	}

	public async Task<bool> DeleteAsync()
	{
		await _userRepo.DeleteAsync(ManagedUser);
		return true;
	}
}
