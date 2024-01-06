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
public class UserController (IMediator Mediator) : ControllerBase
{
    [HttpPost]
	[TranslateResultToActionResult]
	public async Task<Result<User>> Register(string email, string password, string name, string about, DateTime dateOfBirth)
	{
		return await Mediator.Send(new RegisterUserCommand(email, password, name, about, dateOfBirth));
	}

	// I think only Admin should get access to all user's Ids and email addresses.
    [HttpGet]
	[Authorize(Roles = "Admin")]
	[TranslateResultToActionResult]
	public async Task<Result<ICollection<UserDTO>>> GetAll()
	{
		return await Mediator.Send(new GetAllUsersQuery());
	}

    [HttpGet]
	[Authorize]
	[TranslateResultToActionResult]
	public async Task<Result<User>> Get(string id)
	{
		return await Mediator.Send(new GetUserByIdQuery(id));
	}

    [HttpPut]
	[Authorize]
	[TranslateResultToActionResult]
	public async Task<Result<User>> Update(string id, string name, string about, DateTime dateOfBirth)
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		return await Mediator.Send(new UpdateUserCommand(authResult.Value, id, name, about, dateOfBirth));
	}

    [HttpPut]
	[Authorize(Roles = "Admin")]
	[TranslateResultToActionResult]
	public async Task<Result<bool>> AddToRole(string userId, string role)
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		return await Mediator.Send(new AddUserToRoleCommand(authResult.Value, userId, role));
	}

    [HttpPut]
	[Authorize(Roles = "Admin")]
	[TranslateResultToActionResult]
	public async Task<Result<bool>> RemoveFromRole(string userId, string role)
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		return await Mediator.Send(new RemoveUserFromRoleCommand(authResult.Value, userId, role));
	}

    [HttpDelete]
	[Authorize]
	[TranslateResultToActionResult]
	public async Task<Result<bool>> Delete(string id)
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		return await Mediator.Send(new DeleteUserCommand(authResult.Value, id));
	}

}
