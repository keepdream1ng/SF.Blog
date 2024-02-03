using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SF.Blog.Core;
using SF.Blog.Infrastructure.Data.DTO;
using SF.Blog.Infrastructure.Mediator.Queries;
using SF.Blog.UseCases.Posts;

namespace SF.Blog.Web.ApiControllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
public class PostController (IMediator Mediator) : ControllerBase
{
	/// <summary>
	/// Create a post with authorized by cookies user, post content in the body.
	/// </summary>
    [HttpPost]
	[Authorize]
	[TranslateResultToActionResult]
	public async Task<Result<Post>> Create(string title, [FromBody] string content)
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		return await Mediator.Send(new CreatePostCommand(authResult.Value, title, content));
	}

	/// <summary>
	/// Get all posts with no pagination.
	/// </summary>
    [HttpGet]
	[TranslateResultToActionResult]
	public async Task<Result<ICollection<PostDTO>>> GetAll()
	{
		return await Mediator.Send(new GetAllPostsQuery());
	}

	/// <summary>
	/// Get posts by known author guid.
	/// </summary>
    [HttpGet]
	[TranslateResultToActionResult]
	public async Task<Result<ICollection<PostDTO>>> GetAllByAuthorId(string id)
	{
		return await Mediator.Send(new GetPostsByOwnerIdQuery(id));
	}

	/// <summary>
	/// Update known by guid post with authorized by cookies user, post content in the body.
	/// </summary>
	/// <remarks>
	/// Users can edit owned posts, and only moders and admins have full CRUD access.
	/// </remarks>
    [HttpPut]
	[Authorize]
	[TranslateResultToActionResult]
	public async Task<Result<Post>> Update(string id, string title, [FromBody] string content)
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		return await Mediator.Send(new UpdatePostCommand(authResult.Value, id, title, content));
	}

	/// <summary>
	/// Update known by guid post with authorized by cookies user, post content in the body.
	/// </summary>
	/// <remarks>
	/// Users can edit owned posts, and only moders and admins have full CRUD access.
	/// </remarks>
    [HttpDelete]
	[Authorize]
	[TranslateResultToActionResult]
	public async Task<Result<bool>> Delete(string id)
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		return await Mediator.Send(new DeletePostCommand(authResult.Value, id));
	}

}
