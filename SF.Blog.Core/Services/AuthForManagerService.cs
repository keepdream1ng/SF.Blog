namespace SF.Blog.Core;
/// <summary>
/// Service does domain level roles check for giving out instances of manager classes what wrap internal methods for domain aggregates.
/// </summary>
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
		AuthForAccess(userAuth, post);
		var repo = _serviceProvider.GetService(typeof(IRepository<Post>)) as IRepository<Post>;
		return new PostManager(post, repo);
	}

	public CommentManager GetManager(Comment comment, IUserAuth userAuth)
	{
		AuthForAccess(userAuth, comment);
		var repo = _serviceProvider.GetService(typeof(IRepository<Comment>)) as IRepository<Comment>;
		return new CommentManager(comment, repo);
	}

	public UserManager GetManager(User user, IUserAuth userAuth)
	{
		AuthForAccess(userAuth, user, true);
		var repo = _serviceProvider.GetService(typeof(IRepository<User>)) as IRepository<User>;
		return new UserManager(user, repo);
	}

	private bool AuthForAccess(IUserAuth user, IDomainEntity accessTarget, bool needAdminLevel = false)
	{
		// You have access to your own entities.
		if (accessTarget.OwnerId == user.Id) return true;

		// User roles are checked against known admin/moderator level roles.
		Role[] rolesToCheck = needAdminLevel ? _adminAccessRoles : _moderatorAccessRoles;
		bool result = user.Roles.Intersect(rolesToCheck).Any();
		if (result is not true)
		{
			throw new UserAccessDeniedException();
		}
		return result;
	}
}
