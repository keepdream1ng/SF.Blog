namespace SF.Blog.Core;

/// <summary>
/// Service does domain level roles check for giving out instances of manager classes what wrap internal methods for domain aggregates.
/// </summary>
public interface IAuthForManagerService
{
	CommentManager GetManager(Comment comment, IUserAuth userAuth);
	PostManager GetManager(Post post, IUserAuth userAuth);
	UserManager GetManager(User user, IUserAuth userAuth);
}