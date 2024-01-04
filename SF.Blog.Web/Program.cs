using Microsoft.AspNetCore.Identity;
using SF.Blog.Core;
using SF.Blog.Infrastructure;
using SF.Blog.Infrastructure.Data;
using SF.Blog.Infrastructure.Data.Models;
using SF.Blog.UseCases;

namespace SF.Blog.Web;

public static class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.ConfigureServices();
		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			app.UseHsts();
		}
		app.UseSwagger();
		app.UseSwaggerUI();
		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseAuthorization();

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");

		app.Run();
	}

	private static void ConfigureServices(this WebApplicationBuilder builder)
	{
		var services = builder.Services;
		// Core Services.
		services.AddTransient<IAuthForManagerService, AuthForManagerService>();
		// Use Cases Services.
		services.AddUseCasesServices();
		// Ifrastructure Services.
		services.AddInfrastructureServices();
		// Web Services.
		services.AddControllersWithViews();
		services.AddControllers();
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();
		services.AddAuthentication()
			.AddIdentityCookies();
		services.AddIdentity<AppUserModel, IdentityRole>(options =>
			{
				options.SignIn.RequireConfirmedEmail = false;
				options.Password.RequiredLength = 5;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireDigit = false;
			})
			.AddEntityFrameworkStores<ApplicationDbContext>()
			.AddApiEndpoints();
	}
}
