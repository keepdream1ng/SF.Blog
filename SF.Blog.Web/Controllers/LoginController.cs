using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SF.Blog.Infrastructure.Data.Models;
using SF.Blog.Web.Views;

namespace SF.Mod35.TeamNetwork.App.Controllers;

public class LoginController(SignInManager<AppUserModel> Manager) : Controller
{
	[HttpGet]
	public IActionResult Login()
	{
		return View("LoginView");
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(LoginViewModel model)
	{
		if (ModelState.IsValid)
		{
			var result = await Manager.PasswordSignInAsync(model.Email, model.Password, false, false);
			if (result.Succeeded)
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				ModelState.AddModelError(String.Empty, "Incorrect login/passwod pair!");
			}
		}
		return View("LoginView", model);
	}

	[HttpGet]
	public async Task<IActionResult> Logout()
	{
		await Manager.SignOutAsync();
		return RedirectToAction("Index", "Home");
	}
}
