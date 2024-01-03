using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SF.Blog.Core;
using SF.Blog.Infrastructure.Data.Models;
using SF.Blog.Infrastructure.Mapping;

namespace SF.Blog.Infrastructure.Data.Repositories;

/// <summary>
/// Facade repository for domain User entities.
/// </summary>
public class UserRepository : IUserRepository
{
	private readonly UserManager<AppUserModel> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly IMapper _mapper;

	public UserRepository(
		UserManager<AppUserModel> userManager,
		RoleManager<IdentityRole> roleManager,
		IMapper mapper
		)
    {
		_userManager = userManager;
		_roleManager = roleManager;
		_mapper = mapper;
	}
    public async Task<User?> GetByIdAsync(string Id)
	{
		var userModel = await _userManager.FindByIdAsync(Id);
		if ( userModel is null ) return null;
		IList<string> userRoles = await _userManager.GetRolesAsync(userModel);
		return _mapper.Map<UserModelToUserMapperHelper, User>(new(userModel, userRoles));
	}
	public async Task AddToRoleAsync(User user, string role)
	{
		bool roleExists = await _roleManager.RoleExistsAsync(role);
		if (!roleExists)
		{
			await _roleManager.CreateAsync(new IdentityRole(role));
		}
		var userModel = await _userManager.FindByIdAsync(user.Id);
		await _userManager.AddToRoleAsync(userModel, role);
	}

	public async Task RemoveFromRoleAsync(User user, Role role)
	{
		var userModel = await _userManager.FindByIdAsync(user.Id);
		await _userManager.RemoveFromRoleAsync(userModel, role.Name);
	}

	public async Task UpdateAsync(User entity)
	{
		var userModel = await _userManager.FindByIdAsync(entity.Id);
		_mapper.Map<User, AppUserModel>(entity, userModel);
		await _userManager.UpdateAsync(userModel);
	}

	public async Task DeleteAsync(User entity)
	{
		var user = await _userManager.FindByIdAsync(entity.Id);
		if (user is not null)
			await _userManager.DeleteAsync(user);
	}
}
