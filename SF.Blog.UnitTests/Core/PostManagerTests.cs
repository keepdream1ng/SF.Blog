using NSubstitute;

namespace SF.Blog.UnitTests.Core;
public class PostManagerTests
{
	private Post CreateNewPost()
	{
		return new Post("1", "Test", "Tests are important!");
	}

	[Fact]
	public async Task UpdatePostAsync_Should_Update_Post_And_Call_Repository_UpdateAsync()
	{
		// Arrange
		var post = CreateNewPost();
		var postRepo = Substitute.For<IRepository<Post>>();
		var postManager = new PostManager(post, postRepo);

		// Act
		await postManager.UpdatePostAsync("New Title", "New Content");

		// Assert
		Assert.Equal("New Title", post.Title);
		Assert.Equal("New Content", post.Content);
		await postRepo.Received(1).UpdateAsync(post);
	}

	[Fact]
	public async Task AddTagAsync_Should_Add_Tag_And_Call_Repository_UpdateAsync()
	{
		// Arrange
		var post = CreateNewPost();
		var postRepo = Substitute.For<IRepository<Post>>();
		var postManager = new PostManager(post, postRepo);
		string tagString = "NewTag";
		var Tag = new Tag(tagString);
		bool expected = true;

		// Act
		bool actual = await postManager.AddTagAsync(tagString);

		// Assert
		Assert.Equal(expected, actual);
		Assert.Contains(Tag, post.Tags);
		await postRepo.Received(1).UpdateAsync(post);
	}

	[Fact]
	public async Task RemoveTagAsync_Should_Remove_Tag_And_Call_Repository_UpdateAsync()
	{
		// Arrange
		var post = CreateNewPost();
		string tagString = "TagToRemove";
		post.AddTag(tagString);
		var tagToRemove = new Tag(tagString);
		var postRepo = Substitute.For<IRepository<Post>>();
		var postManager = new PostManager(post, postRepo);
		bool expected = true;

		// Act
		bool actual = await postManager.RemoveTagAsync(tagToRemove);

		// Assert
		Assert.Equal(expected, actual);
		Assert.DoesNotContain(tagToRemove, post.Tags);
		await postRepo.Received(1).UpdateAsync(post);
	}

	[Fact]
	public async Task RemoveTagAsync_Should_ReturnFalse_And_DoNotCall_Repository_UpdateAsync()
	{
		// Arrange
		var post = CreateNewPost();
		string tagString = "TagToRemove";
		var tagToRemove = new Tag(tagString);
		var postRepo = Substitute.For<IRepository<Post>>();
		var postManager = new PostManager(post, postRepo);
		bool expected = false;

		// Act
		bool actual = await postManager.RemoveTagAsync(tagToRemove);

		// Assert
		Assert.Equal(expected, actual);
		Assert.DoesNotContain(tagToRemove, post.Tags);
		await postRepo.DidNotReceive().UpdateAsync(post);
	}

	[Fact]
	public async Task DeleteAsync_Should_Call_Repository_DeleteAsync()
	{
		// Arrange
		var post = CreateNewPost();
		var postRepo = Substitute.For<IRepository<Post>>();
		var postManager = new PostManager(post, postRepo);

		// Act
		await postManager.DeleteAsync();

		// Assert
		await postRepo.Received(1).DeleteAsync(post);
	}
}
