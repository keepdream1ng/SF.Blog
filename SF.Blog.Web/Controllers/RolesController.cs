using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SF.Blog.Infrastructure.Data.DTO;
using SF.Blog.Infrastructure.Mediator.Queries;

namespace SF.Blog.Web.Controllers;

public class RolesController(IMediator Mediator) : Controller
{
	[HttpGet]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> GetAll()
	{
        Result<ICollection<RoleDTO>> roleResult = await Mediator.Send(new GetAllRolesQuery());
		return View("AllRolesView", roleResult.Value);
	}
}
