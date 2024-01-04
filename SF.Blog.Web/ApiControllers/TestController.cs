using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVCminimal.ApiControllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{

	[HttpGet]
	[Authorize]
	public string Index()
	{
		return $"Hello {User.Identity!.Name}";
	}
}
