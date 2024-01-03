using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SF.Blog.Infrastructure.Data.Models;
using System.Reflection;

namespace SF.Blog.Infrastructure.Data;
public class ApplicationDbContext : IdentityDbContext<AppUserModel>
{
	public DbSet<CommentModel> Comments => Set<CommentModel>();
	public DbSet<PostModel> Posts => Set<PostModel>();
	public DbSet<TagModel> Tags => Set<TagModel>();

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
