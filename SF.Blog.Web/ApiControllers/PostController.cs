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
public class PostController (IMediator Mediator) : ControllerBase
{
    [HttpPost]
	[Authorize]
	[TranslateResultToActionResult]
	public async Task<Result<Post>> Create(string title, [FromBody] string content)
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		return await Mediator.Send(new CreatePostCommand(authResult.Value, title, content));
	}

    [HttpGet]
	[TranslateResultToActionResult]
	public async Task<Result<ICollection<PostDTO>>> GetAll()
	{
		return await Mediator.Send(new GetAllPostsQuery());
	}

    [HttpGet]
	[TranslateResultToActionResult]
	public async Task<Result<ICollection<PostDTO>>> GetAllByAuthorId(string id)
	{
		return await Mediator.Send(new GetPostsByOwnerIdQuery(id));
	}

    [HttpPut]
	[Authorize]
	[TranslateResultToActionResult]
	public async Task<Result<Post>> Update(string id, string title, [FromBody] string content)
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		return await Mediator.Send(new UpdatePostCommand(authResult.Value, id, title, content));
	}

    [HttpDelete]
	[Authorize]
	[TranslateResultToActionResult]
	public async Task<Result<bool>> Delete(string id)
	{
		Result<IUserAuth> authResult = await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
		return await Mediator.Send(new DeletePostCommand(authResult.Value, id));
	}

}
