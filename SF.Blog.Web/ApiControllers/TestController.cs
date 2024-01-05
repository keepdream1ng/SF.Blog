using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SF.Blog.Core;
using SF.Blog.Infrastructure.Mediator.Queries;

namespace MVCminimal.ApiControllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class TestController(IMediator Mediator) : ControllerBase
{
    [HttpGet]
	[Authorize]
	[TranslateResultToActionResult]
	public async Task<Result<IUserAuth>> Index()
	{
		return await Mediator.Send(new GetIUserAuthByClaimsPricipalQuery(User));
	}

	[HttpGet]
	[Authorize(Roles = "Moderator")]
	public string ModerGet()
	{
		return "hello, moder!";
	}
}
