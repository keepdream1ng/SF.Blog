using NSubstitute;

namespace SF.Blog.UnitTests.Core;
public class AuthForManagerServiceTests
{
	private AuthForManagerService GetAuthService()
	{
		var serviceProvider = Substitute.For<IServiceProvider>();
		var postRepository = Substitute.For<IRepository<Post>>();
		var commentRepository = Substitute.For<IRepository<Comment>>();
		var userRepository = Substitute.For<IRepository<User>>();
		serviceProvider.GetService(typeof(IRepository<Post>)).Returns(postRepository);
		serviceProvider.GetService(typeof(IRepository<Comment>)).Returns(commentRepository);
		serviceProvider.GetService(typeof(IRepository<User>)).Returns(userRepository);

		var authForManagerService = new AuthForManagerService(
			admins: [new Role("Admin")],
			moderators: [new Role("Moderator"), new Role("Admin")],
			serviceProvider: serviceProvider
		);
		return authForManagerService;
	}

	[Theory]
	[InlineData("1", "1", "user")]
	[InlineData("1", "2", "Moderator")]
	[InlineData("1", "2", "Admin")]
	public void GetManager_PostArg_AuthenticatedUserWithAccess_ReturnsPostManager(string ownerId, string authId, string authRole)
	{
		// Arrange
		var post = new Post(ownerId, "Test Title", "Test Content");
		var userAuth = Substitute.For<IUserAuth>();
		userAuth.Id.Returns(authId);
		userAuth.Roles.Returns([new Role(authRole)]);
		AuthForManagerService authForManagerService = GetAuthService();

		// Act
		var result = authForManagerService.GetManager(post, userAuth);

		// Assert
		Assert.NotNull(result);
		Assert.IsType<PostManager>(result);
		Assert.Equal(post, result.ManagedPost);
	}

	[Fact]
	public void GetManager_PostArg_UnauthenticatedUser_ThrowsUserAccessDeniedException()
	{
		// Arrange
		var ownerId1 = "1";
		var ownerId2 = "2";
		var post = new Post(ownerId1, "Test Title", "Test Content");
		var userAuth = Substitute.For<IUserAuth>();
		userAuth.Id.Returns(ownerId2);
		userAuth.Roles.Returns(new[] { new Role("User") });
		AuthForManagerService authForManagerService = GetAuthService();

		// Act & Assert
		Assert.Throws<UserAccessDeniedException>(() => authForManagerService.GetManager(post, userAuth));
	}

	[Theory]
	[InlineData("1", "1", "user")]
	[InlineData("1", "2", "Moderator")]
	[InlineData("1", "2", "Admin")]
	public void GetManager_CommentArg_AuthenticatedUserWithAccess_ReturnsCommentManager(string ownerId, string authId, string authRole)
	{
		// Arrange
		var comment = new Comment(ownerId, "postId", "This is test");
		var userAuth = Substitute.For<IUserAuth>();
		userAuth.Id.Returns(authId);
		userAuth.Roles.Returns([new Role(authRole)]);
		AuthForManagerService authForManagerService = GetAuthService();

		// Act
		var result = authForManagerService.GetManager(comment, userAuth);

		// Assert
		Assert.NotNull(result);
		Assert.IsType<CommentManager>(result);
		Assert.Equal(comment, result.ManagedComment);
	}

	[Fact]
	public void GetManager_CommentArg_UnauthenticatedUser_ThrowsUserAccessDeniedException()
	{
		// Arrange
		var ownerId1 = "1";
		var ownerId2 = "2";
		var comment = new Comment(ownerId1, "postId", "This is test");
		var userAuth = Substitute.For<IUserAuth>();
		userAuth.Id.Returns(ownerId2);
		userAuth.Roles.Returns(new[] { new Role("User") });
		AuthForManagerService authForManagerService = GetAuthService();

		// Act & Assert
		Assert.Throws<UserAccessDeniedException>(() => authForManagerService.GetManager(comment, userAuth));
	}

	[Theory]
	[InlineData("1", "1", "user")]
	[InlineData("1", "2", "Admin")]
	public void GetManager_UserArg_AuthenticatedUserWithAccess_ReturnsCommentManager(string ownerId, string authId, string authRole)
	{
		// Arrange
		var user = new User("TestUser", "this is testing", new DateTime(1990, 1, 1), ownerId);
		var userAuth = Substitute.For<IUserAuth>();
		userAuth.Id.Returns(authId);
		userAuth.Roles.Returns([new Role(authRole)]);
		AuthForManagerService authForManagerService = GetAuthService();

		// Act
		var result = authForManagerService.GetManager(user, userAuth);

		// Assert
		Assert.NotNull(result);
		Assert.IsType<UserManager>(result);
		Assert.Equal(user, result.ManagedUser);
	}

	[Theory]
	[InlineData("1", "2", "user")]
	[InlineData("1", "3", "Moderator")]
	public void GetManager_UserArg_UnauthenticatedUser_ThrowsUserAccessDeniedException(string ownerId, string authId, string authRole)
	{
		// Arrange
		var user = new User("TestUser", "this is testing", new DateTime(1990, 1, 1), ownerId);
		var userAuth = Substitute.For<IUserAuth>();
		userAuth.Id.Returns(authId);
		userAuth.Roles.Returns(new[] { new Role(authRole) });
		AuthForManagerService authForManagerService = GetAuthService();

		// Act & Assert
		Assert.Throws<UserAccessDeniedException>(() => authForManagerService.GetManager(user, userAuth));
	}
}
