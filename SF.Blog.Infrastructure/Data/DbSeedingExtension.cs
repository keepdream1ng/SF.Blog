using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SF.Blog.Infrastructure.Data.Models;

namespace SF.Blog.Infrastructure.Data;
public static class DbSeedingExtension
{
	public static string[] Roles =     ["Admin",          "Moderator",      "User"         ];
	public static string[] Emails =    ["Admin@mail.com", "Moder@mail.com", "User@mail.com"];
	public static string[] Passwords = ["1234Admin!",     "1234Moder!",     "1234User!"    ];

	public async static void SeedDatabaseIfNeeded(this IServiceProvider services)
	{
		// Seeding user roles.
		using (var scope = services.CreateScope())
		{
			var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			foreach (var role in Roles)
			{
				if (!await roleManager.RoleExistsAsync(role))
				{
					await roleManager.CreateAsync(new IdentityRole(role));
				}
			}
		}

		// Seeding Users.
		using (var scope = services.CreateScope())
		{
			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUserModel>>();
			for (int i = 0; i < 3;  i++)
			{
				if (await userManager.FindByEmailAsync(Emails[i]) is null)
				{
					var user = new AppUserModel() {
						Id = $"{i}",
						About = Roles[i],
						Email = Emails[i],
						Name = Emails[i],
						DateOfBirth = DateTime.Today.AddYears(-100),
						EmailConfirmed = true
					};
					await userManager.CreateAsync(user, Passwords[i]);
					await userManager.AddToRoleAsync(user, Roles[i]);
				}
			}
		}
	}
}
