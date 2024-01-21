using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SF.Blog.Web.Models;
using System.Diagnostics;

namespace SF.Blog.Web.Controllers;
public class HomeController : Controller
{
	public IActionResult Index()
	{
		return RedirectToAction("Index", "Posts");
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}

	public IActionResult Oops(string code)
	{
		return code switch
		{
			"403" => View("Response403"),
			"404" => View("Response404"),
			_     => View("ResponseError", code)
		};
	}

	[Authorize(Roles = "Admin")]
	public IActionResult Throw()
	{
		throw new Exception("Secret test exception here");
	}
}
