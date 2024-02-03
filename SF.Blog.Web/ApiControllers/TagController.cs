using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SF.Blog.Core;
using SF.Blog.Infrastructure.Data.DTO;
using SF.Blog.Infrastructure.Mediator.Queries;
using SF.Blog.UseCases.Tags;

namespace SF.Blog.Web.ApiControllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
public class TagController (IMediator Mediator) : ControllerBase
{
	/// <summary>
	/// Create a tag for known by guid post with authorized by cookies user.
	/// </summary>
	/// <remarks>
	/// Users can edit owned posts, and only moders and admins have full CRUD access.
	/// </remarks>
    [HttpPost]
	[Authorize]
	[TranslateResultToActionResult]
	public async Task<Result<bool>> AddToPost(string postId, string tag)
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		return await Mediator.Send(new AddNewTagCommand(authResult.Value, postId, tag));
	}

	/// <summary>
	/// Get all connentions for posts and tags entities, with no pagination.
	/// </summary>
	[HttpGet]
	[TranslateResultToActionResult]
	public async Task<Result<ICollection<TagPostDTO>>> GetAll()
	{
		return await Mediator.Send(new GetAllTagPostsQuery());
	}

	// Have no idea why someone would need tag by its id, so im not implementing it.
	// In this architecture tags are unseparable with posts anyway.

	/// <summary>
	/// Edit a tag for known by guid post with authorized by cookies user.
	/// </summary>
	/// <remarks>
	/// Users can edit owned posts, and only moders and admins have full CRUD access.
	/// </remarks>
    [HttpPut]
	[Authorize]
	[TranslateResultToActionResult]
	public async Task<Result<bool>> UpdateOnPost(string postId, string tagToUpdate, string newValue)
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		return await Mediator.Send(new UpdateTagCommand(authResult.Value, postId, tagToUpdate, newValue));
	}

	/// <summary>
	/// Delete a tag for known by guid post with authorized by cookies user.
	/// </summary>
	/// <remarks>
	/// Users can edit owned posts, and only moders and admins have full CRUD access.
	/// </remarks>
    [HttpDelete]
	[Authorize]
	[TranslateResultToActionResult]
	public async Task<Result<bool>> RemoveFromPost(string postId, string tagToDelete)
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		return await Mediator.Send(new RemoveTagCommand(authResult.Value, postId, tagToDelete));
	}
}
