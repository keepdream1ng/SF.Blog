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
		if (AuthForAccess(userAuth, post))
		{
			var repo = _serviceProvider.GetService(typeof(IRepository<Post>)) as IRepository<Post>;
			return new PostManager(post, repo);
		}
		else
			throw new UserAccessDeniedException();
	}
	public CommentManager GetManager(Comment comment, IUserAuth userAuth)
	{
		if (AuthForAccess(userAuth, comment))
		{
			var repo = _serviceProvider.GetService(typeof(IRepository<Comment>)) as IRepository<Comment>;
			return new CommentManager(comment, repo);
		}
		else
			throw new UserAccessDeniedException();
	}
	public UserManager GetManager(User user, IUserAuth userAuth)
	{
		if (AuthForAccess(userAuth, user, true))
		{
			var repo = _serviceProvider.GetService(typeof(IRepository<User>)) as IRepository<User>;
			return new UserManager(user, repo);
		}
		else
			throw new UserAccessDeniedException();
	}

    private bool AuthForAccess(IUserAuth user, IDomainEntity accessTarget, bool needAdminLevel = false)
	{
		// You have access to your own entities.
		if (accessTarget.OwnerId == user.Id) return true;

		// User roles are checked against known admin/moderator level roles.
		Role[] rolesToCheck = needAdminLevel ? _adminAccessRoles : _moderatorAccessRoles;
		return user.Roles.Intersect(rolesToCheck).Any();
	}
}
