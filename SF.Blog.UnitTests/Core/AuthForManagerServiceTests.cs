using NSubstitute;

namespace SF.Blog.UnitTests.Core;
public class AuthForManagerServiceTests
{
	private AuthForManagerService GetAuthService()
	{
		var serviceProvider = Substitute.For<IServiceProvider>();
		var postRepository = Substitute.For<IRepository<Post>>();
		serviceProvider.GetService(typeof(IRepository<Post>)).Returns(postRepository);

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
}
