using MediatR;
using Microsoft.AspNetCore.Mvc;
using SF.Blog.Infrastructure.Mediator.Commands;
using SF.Blog.Web.Views.User;
using Ardalis.Result;

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
}
