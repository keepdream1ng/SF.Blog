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
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
		services.AddControllersWithViews();
		services.AddControllers();
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();
	}
}
