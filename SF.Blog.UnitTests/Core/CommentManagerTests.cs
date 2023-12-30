using NSubstitute;

namespace SF.Blog.UnitTests.Core;
public class CommentManagerTests
{
	private Comment CreateNewComment()
	{
		var ownerId = "ownerId";
		var replyToId = "replyToId";
		var text = "Initial text";
		var comment = new Comment(ownerId, replyToId, text);
		return comment;
	}

	[Fact]
	public async Task UpdateCommentAsync_Should_UpdateCommentAndRepository()
	{
		// Arrange
		Comment comment = CreateNewComment();
		var commentRepo = Substitute.For<IRepository<Comment>>();
		var commentManager = new CommentManager(comment, commentRepo);
		var newText = "Updated text";

		// Act
		await commentManager.UpdateCommentAsync(newText);

		// Assert
		Assert.Equal(newText, comment.Text);
		await commentRepo.Received(1).UpdateAsync(Arg.Is<Comment>(c => c.Text == newText));
	}

	[Fact]
	public async Task DeleteAsync_Should_DeleteCommentFromRepository()
	{
		// Arrange
		Comment comment = CreateNewComment();
		var commentRepo = Substitute.For<IRepository<Comment>>();
		var commentManager = new CommentManager(comment, commentRepo);

		// Act
		var result = await commentManager.DeleteAsync();

		// Assert
		Assert.True(result);
		await commentRepo.Received(1).DeleteAsync(comment);
	}
}
