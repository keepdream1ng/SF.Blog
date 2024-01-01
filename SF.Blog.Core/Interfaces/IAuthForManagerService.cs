namespace SF.Blog.Core;

public interface IAuthForManagerService
{
	CommentManager GetManager(Comment comment, IUserAuth userAuth);
	PostManager GetManager(Post post, IUserAuth userAuth);
	UserManager GetManager(User user, IUserAuth userAuth);
}