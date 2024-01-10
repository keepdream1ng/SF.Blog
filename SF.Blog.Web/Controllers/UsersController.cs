using MediatR;
using Microsoft.AspNetCore.Mvc;
using SF.Blog.Infrastructure.Mediator.Commands;
using SF.Blog.Web.Views.User;
using Ardalis.Result;
using SF.Blog.Core;
using SF.Blog.Infrastructure.Mediator.Queries;
using Microsoft.AspNetCore.Authorization;
using SF.Blog.Web.Views.Users;
using SF.Blog.UseCases.Users;
using SF.Blog.Infrastructure.Data.DTO;

namespace SF.Blog.Web.Controllers;

public class UsersController (IMediator Mediator) : Controller
{
	[HttpGet]
	public IActionResult Register()
	{
		return View("RegisterView");
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Register(RegisterViewModel model)
	{
		if (ModelState.IsValid)
		{
			Result<Core.User> result = await Mediator.Send(
				new RegisterUserCommand(
					model.EmailReg,
					model.PasswordConfirm,
					model.Name,
					model.About,
					model.DateOfBirth));
			if (result.IsSuccess) return RedirectToAction("Index", "Home");
			// Passing domain level validation, like "user exists" back to user.
			result.Errors
					.ToList()
					.ForEach(e => ModelState.AddModelError(String.Empty, e));
		}
		return View("RegisterView", model);
	}

	[HttpGet]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> GetAll()
	{
		Result<ICollection<UserDTO>> userResult = await Mediator.Send(new GetAllUsersQuery());
		return View("GetAllUsersView", new GetAllUsersViewModel() { Users = userResult.Value});
	}

	[HttpGet]
	[Authorize]
	public async Task<IActionResult> Edit(string id)
	{
		if (string.IsNullOrEmpty(id))
		{
			Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
			id = authResult.Value.Id;
		}
		Result<User> userResult = await Mediator.Send(new GetUserByIdQuery(id));
		if (!userResult.IsSuccess) return BadRequest();
		return View("EditUserView", new EditUserViewModel(userResult.Value));
	}

	[HttpPost]
	[Authorize]
	public async Task<IActionResult> Edit(EditUserViewModel model)
	{
		if (ModelState.IsValid)
		{
			Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
			Result<Core.User> result = await Mediator.Send(
				new UpdateUserCommand(
					authResult.Value,
					model.Id,
					model.Name,
					model.About,
					model.DateOfBirth));
			if (result.IsSuccess) return RedirectToAction("Index", "Home");
			// Passing domain level validation back to user.
			result.Errors
					.ToList()
					.ForEach(e => ModelState.AddModelError(String.Empty, e));
		}

		Result<User> userResult = await Mediator.Send(new GetUserByIdQuery(model.Id));
		if (!userResult.IsSuccess) return BadRequest();
		return View("EditUserView", new EditUserViewModel(userResult.Value));
	}

	[HttpPost]
	[Authorize]
	public async Task<IActionResult> Delete(DeleteUserViewModel model)
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		Result<bool> result = await Mediator.Send(new DeleteUserCommand(authResult.Value, model.Id));
		if (!result.IsSuccess) return BadRequest();
		if (User.IsInRole("Admin")) return RedirectToAction("GetAll", "Users");
		return RedirectToAction("Logout", "Session");
	}
}
