using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SF.Blog.Infrastructure.Data.Models;
using SF.Blog.Web.Models;
using System.Diagnostics;

namespace SF.Blog.Web.Controllers;
public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;
	private readonly SignInManager<AppUserModel> _signInManager;

	public HomeController(ILogger<HomeController> logger, SignInManager<AppUserModel> signInManager)
	{
		_logger = logger;
		_signInManager = signInManager;
	}

	public IActionResult Index()
	{
		return View();
	}

	public IActionResult Privacy()
	{
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
