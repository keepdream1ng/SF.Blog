using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using NLog.Web;
using SF.Blog.Core;
using SF.Blog.Infrastructure;
using SF.Blog.Infrastructure.Data;
using SF.Blog.Infrastructure.Data.Models;
using SF.Blog.UseCases;
using System.Reflection;

namespace SF.Blog.Web;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.ConfigureServices();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }
        else
        {
            app.UseExceptionHandler(_ => { });
        }
        app.UseHsts();

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        // Using dotnet8 build in authentication api.
        app.MapGroup("api/auth")
            .MapIdentityApi<AppUserModel>();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.UseStatusCodePagesWithReExecute("/Home/Oops", "?code={0}");

        app.Services.SeedDatabaseIfNeeded();

        NLog.LogManager.Setup()
            .LoadConfigurationFromAppSettings();

        app.Run();
    }

    private static void ConfigureServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        // Core Services.
        services.AddSingleton<IRoleConfigProvider, RoleConfigProvider>();
        services.AddTransient<IAuthForManagerService, AuthForManagerService>();
        // Use Cases Services.
        services.AddUseCasesServices();
        // Ifrastructure Services.
        services.AddInfrastructureServices();
        // Web Services.
        services.AddExceptionHandler<Middleware.GlobalExceptionHandler>();
        services.AddControllersWithViews();
        services.AddControllers(mvcOptions => mvcOptions.AddDefaultResultConvention());
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Blogspace API",
                Description = "Endpoint for the blog app, also some pages have calls to them with ajax",
            });

			var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
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

        // Logging.
        builder.Logging.ClearProviders();
        builder.Host.UseNLog();
    }
}
