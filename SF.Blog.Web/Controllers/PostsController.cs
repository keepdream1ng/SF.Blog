using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ardalis.Result;
using SF.Blog.Core;
using SF.Blog.Infrastructure.Mediator.Queries;
using Microsoft.AspNetCore.Authorization;
using SF.Blog.Web.Views.Posts;
using SF.Blog.UseCases.Posts;

namespace SF.Blog.Web.Controllers;

public class PostsController (IMediator Mediator) : Controller
{
	[HttpGet]
	public IActionResult New()
	{
		if (User.Identity.IsAuthenticated)
		{
			return View("CreatePostView");
		}
		return RedirectToAction("Login", "Session");
	}

	[HttpPost]
	[Authorize]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> New(CreatePostViewModel model)
	{
		if (ModelState.IsValid)
		{
			Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
			Result<Post> result = await Mediator.Send(new CreatePostCommand(authResult.Value, model.Title, model.Content));
			if (result.IsSuccess) return RedirectToAction("Index", "Home");
			// Passing domain level validation, back to user.
			result.Errors
					.ToList()
					.ForEach(e => ModelState.AddModelError(String.Empty, e));
		}
		return View("CreatePostView", model);
	}

}
