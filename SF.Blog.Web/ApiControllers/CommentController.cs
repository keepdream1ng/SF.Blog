using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SF.Blog.Core;
using SF.Blog.Infrastructure.Data.DTO;
using SF.Blog.Infrastructure.Mediator.Queries;
using SF.Blog.UseCases.Comments;

namespace SF.Blog.Web.ApiControllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CommentController (IMediator Mediator) : ControllerBase
{
    [HttpPost]
	[Authorize]
	[TranslateResultToActionResult]
	public async Task<Result<Comment>> Create(string postId, [FromBody] string comment)
	{
		if (!await Mediator.Send(new PostExistWithIdQuery(postId))) return Result.Invalid();
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		return await Mediator.Send(new CreateCommentCommand(authResult.Value, postId, comment));
	}

	[HttpGet]
	[TranslateResultToActionResult]
	public async Task<Result<ICollection<CommentDTO>>> GetAll()
	{
		return await Mediator.Send(new GetAllCommentsQuery());
	}

	[HttpGet]
	[TranslateResultToActionResult]
	public async Task<Result<Comment>> Get(string id)
	{
		return await Mediator.Send(new GetCommentByIdQuery(id));
	}

    [HttpPut]
	[Authorize]
	[TranslateResultToActionResult]
	public async Task<Result<Comment>> Update(string id, [FromBody] string content)
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		return await Mediator.Send(new UpdateCommentCommand(authResult.Value, id, content));
	}

    [HttpDelete]
	[Authorize]
	[TranslateResultToActionResult]
	public async Task<Result<bool>> Delete(string id)
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		return await Mediator.Send(new DeleteCommentCommand(authResult.Value, id));
	}
}
