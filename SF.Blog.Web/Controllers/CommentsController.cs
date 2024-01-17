using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SF.Blog.Core;
using SF.Blog.Infrastructure.Data.DTO;
using SF.Blog.Infrastructure.Mediator.Queries;
using SF.Blog.UseCases.Comments;
using SF.Blog.Web.Views.Shared;

namespace SF.Blog.Web.Controllers;
public class CommentsController (IMediator Mediator) : Controller
{
	[HttpGet]
	[Authorize]
	public async Task<IActionResult> MyComments()
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		Result<ICollection<CommentDTO>> result = await Mediator.Send(new GetCommentsByOwnerIdQuery(authResult.Value.Id));
		return View("AllCommentsView", new AllCommentsViewModel(result.Value, true));
	}

	[HttpGet]
	[Authorize(Roles = "Admin,Moderator")]
	public async Task<IActionResult> GetAll()
	{
		Result<ICollection<CommentDTO>> result = await Mediator.Send(new GetAllCommentsQuery());
		return View("AllCommentsView", new AllCommentsViewModel(result.Value, true));
	}

	[HttpPost]
	[Authorize]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> New(CreateEditCommentViewModel model)
	{
		if (ModelState.IsValid)
		{
			Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
			Result<Comment> result = await Mediator.Send(
				new CreateCommentCommand(
					authResult.Value,
					model.Id,
					model.CommentText));
			if (result.IsSuccess) return RedirectToAction("Post", "Posts", new {id = result.Value.ReplyToId});
			// Passing domain level validation, back to user.
			result.Errors
					.ToList()
					.ForEach(e => ModelState.AddModelError(String.Empty, e));
		}
		return View("CreateEditCommentView", model);
	}

	[HttpGet]
	[Authorize]
	public async Task<IActionResult> Update(string id)
	{
		Result<CommentDTO> result = await Mediator.Send(new GetCommentDtoByIdQuery(id));
		if (!result.IsSuccess) return NotFound();
		return View("CreateEditCommentView", new CreateEditCommentViewModel(result.Value));
	}


	[HttpPost]
	[Authorize]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Update(CreateEditCommentViewModel model)
	{
		if (ModelState.IsValid)
		{
			Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
			Result<Comment> result = await Mediator.Send(
				new UpdateCommentCommand(
					authResult.Value,
					model.Id,
					model.CommentText));
			if (result.IsSuccess) return RedirectToAction("Post", "Posts", new {id = result.Value.ReplyToId});
			// Passing domain level validation, back to user.
			result.Errors
					.ToList()
					.ForEach(e => ModelState.AddModelError(String.Empty, e));
		}
		return View("CreateEditCommentView", model);
	}

	[HttpPost]
	[Authorize]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Delete(DeleteCommentViewModel model)
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		Result<bool> result = await Mediator.Send(new DeleteCommentCommand(authResult.Value, model.Id));
		if (result.IsSuccess)
		{
			return RedirectToAction("Post", "Posts", new {id = model.PostId});
		}
		else
		{
			return BadRequest();
		}
	}

}
