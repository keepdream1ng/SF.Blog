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
public class TagController (IMediator Mediator) : ControllerBase
{
    [HttpPost]
	[Authorize]
	[TranslateResultToActionResult]
	public async Task<Result<bool>> AddToPost(string postId, string tag)
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		return await Mediator.Send(new AddNewTagCommand(authResult.Value, postId, tag));
	}

	[HttpGet]
	[TranslateResultToActionResult]
	public async Task<Result<ICollection<TagPostDTO>>> GetAll()
	{
		return await Mediator.Send(new GetAllTagPostsQuery());
	}

	// Have no idea why someone would need tag by its id, so im not implementing it.
	// In this architecture tags are unseparable with posts anyway.

    [HttpPut]
	[Authorize]
	[TranslateResultToActionResult]
	public async Task<Result<bool>> UpdateOnPost(string postId, string tagToUpdate, string newValue)
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		return await Mediator.Send(new UpdateTagCommand(authResult.Value, postId, tagToUpdate, newValue));
	}

    [HttpDelete]
	[Authorize]
	[TranslateResultToActionResult]
	public async Task<Result<bool>> RemoveFromPost(string postId, string tagToDelete)
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		return await Mediator.Send(new RemoveTagCommand(authResult.Value, postId, tagToDelete));
	}
}
