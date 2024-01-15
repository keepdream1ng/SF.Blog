using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SF.Blog.Core;
using SF.Blog.Infrastructure.Mediator.Queries;
using SF.Blog.UseCases.Comments;
using SF.Blog.Web.Views.Shared;

namespace SF.Blog.Web.Controllers;
public class CommentsController (IMediator Mediator) : Controller
{
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
}
