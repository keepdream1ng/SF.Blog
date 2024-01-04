using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SF.Blog.UseCases;
public static class UseCasesServicesExtension
{
	public static void AddUseCasesServices(this IServiceCollection services)
	{
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
	}

}
