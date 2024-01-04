using SF.Blog.Core;

namespace SF.Blog.Web;
public class RoleConfigProvider : IRoleConfigProvider
{
	private readonly IConfiguration _configuration;

	public RoleConfigProvider(IConfiguration configuration)
    {
		_configuration = configuration;
	}
    public Role[] GetAdminRoles()
	{
		return _configuration.GetSection("AdminRoles")
			.Get<string[]>()
			.Select(str => new Role(str))
			.ToArray();
	}

	public Role[] GetModeratorRoles()
	{
		return _configuration.GetSection("ModeratorRoles")
			.Get<string[]>()
			.Select(str => new Role(str))
			.ToArray();
	}
}
