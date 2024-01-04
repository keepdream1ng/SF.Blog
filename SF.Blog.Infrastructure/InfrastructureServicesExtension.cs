using Ardalis.Specification;
using Microsoft.Extensions.DependencyInjection;
using SF.Blog.Core;
using SF.Blog.Infrastructure.Data;
using SF.Blog.Infrastructure.Data.Models;
using SF.Blog.Infrastructure.Data.Repositories;
using SF.Blog.Infrastructure.Mapping;
using System.Reflection;

namespace SF.Blog.Infrastructure;
public static class InfrastructureServicesExtension
{
	public static void AddInfrastructureServices(this IServiceCollection services)
	{
		services.AddDbContext<ApplicationDbContext>();
		services.AddTransient<IRepositoryBase<PostModel>, EfRepository<PostModel>>();
		services.AddTransient<IRepositoryBase<CommentModel>, EfRepository<CommentModel>>();
		services.AddTransient<IRepositoryBase<TagModel>, EfRepository<TagModel>>();
		services.AddTransient<IRepositoryBase<TagPost>, EfRepository<TagPost>>();
		services.AddTransient<IUserRepository, UserRepository>();
		services.AddTransient<IPostRepository, PostRepository>();
		services.AddTransient<IRepository<Comment>, CommentRepository>();
		services.AddAutoMapper(Assembly.GetAssembly(typeof(InfrastructureMappingProfile)));
	}
}
