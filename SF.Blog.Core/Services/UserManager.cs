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

	public async Task<bool> AddRoleAsync(string roleName)
	{
		bool result = ManagedUser.AddRole(roleName);
		if (result)
		{
			await _userRepo.AddToRoleAsync(ManagedUser, roleName);
		}
		return result;
	}
	public async Task<bool> RemoveRoleAsync(Role role)
	{
		bool result = ManagedUser.RemoveRole(role);
		if (result)
		{
			await _userRepo.RemoveFromRoleAsync(ManagedUser, role);
		}
		return result;
	}

	public async Task<bool> DeleteAsync()
	{
		await _userRepo.DeleteAsync(ManagedUser);
		return true;
	}
}
