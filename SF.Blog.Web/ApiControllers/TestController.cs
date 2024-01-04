using Microsoft.AspNetCore.Mvc;

namespace MVCminimal.ApiControllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{

	[HttpGet]
	public string Index()
	{
		return "Hello world again";
	}
}
