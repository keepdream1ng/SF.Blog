using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SF.Blog.Core;
using SF.Blog.Infrastructure.Data.DTO;
using SF.Blog.Infrastructure.Mediator.Commands;
using SF.Blog.Infrastructure.Mediator.Queries;
using SF.Blog.UseCases.Users;

namespace SF.Blog.Web.ApiControllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
public class UserController (IMediator Mediator) : ControllerBase
{
	/// <summary>
	/// User creation endpoint with name, about and date of birth defined.
	/// </summary>
    [HttpPost]
	[TranslateResultToActionResult]
	public async Task<Result<User>> Register(string email, string password, string name, string about, DateTime dateOfBirth)
	{
		return await Mediator.Send(new RegisterUserCommand(email, password, name, about, dateOfBirth));
	}

	/// <summary>
	/// Get all Users info, for admins only.
	/// </summary>
    [HttpGet]
	[Authorize(Roles = "Admin")]
	[TranslateResultToActionResult]
	public async Task<Result<ICollection<UserDTO>>> GetAll()
	{
		return await Mediator.Send(new GetAllUsersQuery());
	}

	/// <summary>
	/// Get known by guid User info with authorized by cookies user.
	/// </summary>
    [HttpGet]
	[Authorize]
	[TranslateResultToActionResult]
	public async Task<Result<User>> Get(string id)
	{
		return await Mediator.Send(new GetUserByIdQuery(id));
	}

	/// <summary>
	/// Update known by guid User info with authorized by cookies user.
	/// </summary>
	/// <remarks>
	/// Users can edit owned profiles, and only admins have full CRUD access.
	/// </remarks>
    [HttpPut]
	[Authorize]
	[TranslateResultToActionResult]
	public async Task<Result<User>> Update(string id, string name, string about, DateTime dateOfBirth)
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		return await Mediator.Send(new UpdateUserCommand(authResult.Value, id, name, about, dateOfBirth));
	}

	/// <summary>
	/// Add known by guid User to role with authorized by cookies Admin.
	/// </summary>
    [HttpPut]
	[Authorize(Roles = "Admin")]
	[TranslateResultToActionResult]
	public async Task<Result<bool>> AddToRole(string userId, string role)
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		return await Mediator.Send(new AddUserToRoleCommand(authResult.Value, userId, role));
	}

	/// <summary>
	/// Remove known by guid User from role with authorized by cookies Admin.
	/// </summary>
    [HttpPut]
	[Authorize(Roles = "Admin")]
	[TranslateResultToActionResult]
	public async Task<Result<bool>> RemoveFromRole(string userId, string role)
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		return await Mediator.Send(new RemoveUserFromRoleCommand(authResult.Value, userId, role));
	}

	/// <summary>
	/// Delete known by guid User with authorized by cookies user.
	/// </summary>
	/// <remarks>
	/// Users can edit owned accounts, and only admins have full CRUD access.
	/// </remarks>
    [HttpDelete]
	[Authorize]
	[TranslateResultToActionResult]
	public async Task<Result<bool>> Delete(string id)
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		return await Mediator.Send(new DeleteUserCommand(authResult.Value, id));
	}

}
