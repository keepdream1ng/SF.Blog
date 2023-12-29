namespace SF.Blog.Core;
public class AuthForManagerService
{
	private readonly Role[] _adminAccessRoles;
	private readonly Role[] _moderatorAccessRoles;
	private readonly IServiceProvider _serviceProvider;

	// Roles definition can be provided via configuration.
	public AuthForManagerService(Role[] admins, Role[] moderators, IServiceProvider serviceProvider)
    {
		_adminAccessRoles = admins;
		_moderatorAccessRoles = moderators;
		_serviceProvider = serviceProvider;
	}

	public PostManager GetManager(Post post, IUserAuth userAuth)
	{
		if (AuthForAcces(userAuth, post))
		{
			var repo = _serviceProvider.GetService(typeof(IRepository<Post>)) as IRepository<Post>;
			return new PostManager(post, repo);
		}
		else
			throw new UserAccessDeniedException();
	}

    private bool AuthForAcces(IUserAuth user, IDomainEntity accessTarget, bool needAdminLevel = false)
	{
		// You have access to your own entities.
		if (accessTarget.OwnerId == user.Id) return true;

		// User roles are checked against known admin/moderator level roles.
		Role[] rolesToCheck = needAdminLevel ? _adminAccessRoles : _moderatorAccessRoles;
		return user.Roles.Intersect(rolesToCheck).Any()? true : false;
	}
}
