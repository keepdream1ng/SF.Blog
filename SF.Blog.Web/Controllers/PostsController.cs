using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ardalis.Result;
using SF.Blog.Core;
using SF.Blog.Infrastructure.Mediator.Queries;
using SF.Blog.Infrastructure.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using SF.Blog.Web.Views.Posts;
using SF.Blog.UseCases.Posts;
using SF.Blog.Infrastructure.Data.Models;
using SF.Blog.Web.Views.Shared;

namespace SF.Blog.Web.Controllers;

public class PostsController(IMediator Mediator) : Controller
{
	[HttpGet]
	public async Task<IActionResult> Index()
	{
		Result<ICollection<PostDTO>> result = await Mediator.Send(new GetAllPostsQuery());
		if (!result.IsSuccess) return BadRequest();
		return View("Index", new AllPostsViewModel(result.Value));
	}

	[HttpGet]
	public IActionResult New()
	{
		if (User.Identity.IsAuthenticated)
		{
			return View("CreatePostView");
		}
		return RedirectToAction("Login", "Session");
	}

	[HttpGet]
	public async Task<IActionResult> Post(string id)
	{
		Result<PostModel> result = await Mediator.Send(new GetPostModelByIdQuery(id));
		if (!result.IsSuccess) return BadRequest();
		return View("PostView", result.Value);
	}

	[HttpGet]
	[Authorize]
	public async Task<IActionResult> MyPosts()
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		Result<ICollection<PostDTO>> result = await Mediator.Send(new GetPostsByOwnerIdQuery(authResult.Value.Id));
		return View("AllPostsView", new AllPostsViewModel(result.Value, true));
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
			if (result.IsSuccess) return RedirectToAction("Update", "Posts", new {id = result.Value.Id});
			// Passing domain level validation, back to user.
			result.Errors
					.ToList()
					.ForEach(e => ModelState.AddModelError(String.Empty, e));
		}
		return View("CreatePostView", model);
	}

	[HttpGet]
	[Authorize]
	public async Task<IActionResult> Update(string id)
	{
		var result = await Mediator.Send(new GetPostDtoByIdQuery(id));
		if (!result.IsSuccess) return BadRequest();
		return View("EditPostView", new EditPostViewModel(result.Value));
	}

	[HttpPost]
	[Authorize]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Update(EditPostViewModel model)
	{
		if (ModelState.IsValid)
		{
			Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
			Result<Post> result = await Mediator.Send(new UpdatePostCommand(authResult.Value, model.PostId, model.Title, model.Content));
			if (result.IsSuccess) return RedirectToAction("Index", "Home");
			// Passing domain level validation, back to user.
			result.Errors
					.ToList()
					.ForEach(e => ModelState.AddModelError(String.Empty, e));
		}
		return View("EditPostView", model);
	}

	[HttpPost]
	[Authorize]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Delete(DeletePostViewModel model)
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		Result<bool> result = await Mediator.Send(new DeletePostCommand(authResult.Value, model.Id));
		if (result.IsSuccess)
		{
			return RedirectToAction("Index", "Home");
		}
		else
		{
			return BadRequest();
		}
	}
}
