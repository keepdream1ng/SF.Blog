using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SF.Blog.Infrastructure.Data.Models;
using System.Reflection;

namespace SF.Blog.Infrastructure.Data;
public class ApplicationDbContext : IdentityDbContext<AppUserModel>
{
	DbSet<CommentModel> Comments { get; set; }
	DbSet<PostModel> Posts { get; set; }
	DbSet<TagModel> Tags { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite("Data Source=blogApp.db");
	}
}
