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
[Produces("application/json")]
public class CommentController (IMediator Mediator) : ControllerBase
{
	/// <summary>
	/// Create comment with authorized by cookies user, comment text is in the body.
	/// </summary>
    [HttpPost]
	[Authorize]
	[TranslateResultToActionResult]
	public async Task<Result<Comment>> Create(string postId, [FromBody] string comment)
	{
		if (!await Mediator.Send(new PostExistWithIdQuery(postId))) return Result.Invalid();
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		return await Mediator.Send(new CreateCommentCommand(authResult.Value, postId, comment));
	}

	/// <summary>
	/// Get all comments DTO, only for moders and admins.
	/// </summary>
	[HttpGet]
	[Authorize(Roles = "Admin,Moderator")]
	[TranslateResultToActionResult]
	public async Task<Result<ICollection<CommentDTO>>> GetAll()
	{
		return await Mediator.Send(new GetAllCommentsQuery());
	}

	/// <summary>
	/// Get collection of comments for post with known guid.
	/// </summary>
	[HttpGet]
	[TranslateResultToActionResult]
	public async Task<Result<ICollection<CommentDTO>>> GetByPostId(string id)
	{
		return await Mediator.Send(new GetCommentsByPostIdQuery(id));
	}

	/// <summary>
	/// Get collection of comments by User with known guid.
	/// </summary>
	[HttpGet]
	[TranslateResultToActionResult]
	public async Task<Result<ICollection<CommentDTO>>> GetByOwnerId(string id)
	{
		return await Mediator.Send(new GetCommentsByOwnerIdQuery(id));
	}

	/// <summary>
	/// Get comment objects with all data by its known guid.
	/// </summary>
	[HttpGet]
	[TranslateResultToActionResult]
	public async Task<Result<Comment>> Get(string id)
	{
		return await Mediator.Send(new GetCommentByIdQuery(id));
	}

	/// <summary>
	/// Update known by guid comment with authorized by cookies user, comment text is in the body.
	/// </summary>
	/// <remarks>
	/// Users can edit owned comments, and only moders and admins have full CRUD access.
	/// </remarks>
    [HttpPut]
	[Authorize]
	[TranslateResultToActionResult]
	public async Task<Result<Comment>> Update(string id, [FromBody] string content)
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		return await Mediator.Send(new UpdateCommentCommand(authResult.Value, id, content));
	}

	/// <summary>
	/// Delete known by guid comment with authorized by cookies user, comment text is in the body.
	/// </summary>
	/// <remarks>
	/// Users can edit owned comments, and only moders and admins have full CRUD access.
	/// </remarks>
    [HttpDelete]
	[Authorize]
	[TranslateResultToActionResult]
	public async Task<Result<bool>> Delete(string id)
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		return await Mediator.Send(new DeleteCommentCommand(authResult.Value, id));
	}
}
