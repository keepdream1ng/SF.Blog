namespace SF.Blog.UnitTests.Core;
public class CommentTests
{
	[Fact]
	public void Constructor_WithValidArguments_ShouldCreateComment()
	{
		// Arrange
		string ownerId = "user1";
		string replyToId = "parentCommentId";
		string text = "This is a comment";

		// Act
		var comment = new Comment(ownerId, replyToId, text);

		// Assert
		Assert.NotNull(comment);
		Assert.Equal(ownerId, comment.OwnerId);
		Assert.Equal(replyToId, comment.ReplyToId);
		Assert.Equal(text, comment.Text);
		Assert.Equal(DateTime.Now.Date, comment.Published.Date); // Check date, ignoring time
		Assert.Null(comment.Modified); // Modified should be null initially
	}

	[Theory]
	[InlineData(" ", "replyToId", "Text")] // Whitespace ownerId
	[InlineData("ownerId", " ", "Text")] // Whitespace replyToId
	[InlineData("ownerId", "replyToId", " ")] // Whitespace text
	[InlineData("", "replyToId", "Text")] // Empty ownerId
	[InlineData("ownerId", "", "Text")] // Empty replyToId
	[InlineData("ownerId", "replyToId", "")] // Empty text
	public void Constructor_WithInvalidArguments_ShouldThrowArgumentException(string ownerId, string replyToId, string text)
	{
		// Act & Assert
		Assert.Throws<ArgumentException>(() => new Comment(ownerId, replyToId, text));
	}

	[Theory]
	[InlineData(null, "replyToId", "Text")] // Null ownerId
	[InlineData("ownerId", null, "Text")] // Null replyToId
	[InlineData("ownerId", "replyToId", null)] // Null text
	public void Constructor_WithNullArguments_ShouldThrowArgumentNullException(string ownerId, string replyToId, string text)
	{
		// Act & Assert
		Assert.Throws<ArgumentNullException>(() => new Comment(ownerId, replyToId, text));
	}

	[Fact]
	public void Update_WithValidText_ShouldUpdateTextAndSetModified()
	{
		// Arrange
		var comment = new Comment("ownerId", "replyToId", "Initial text");
		string updatedText = "Updated text";

		// Act
		comment.Update(updatedText);

		// Assert
		Assert.Equal(updatedText, comment.Text);
		Assert.NotNull(comment.Modified);
	}

	[Theory]
	[InlineData(" ")] // Whitespace text
	[InlineData("")] // Empty text
	public void Update_WithInvalidText_ShouldThrowArgumentException(string text)
	{
		// Arrange
		var comment = new Comment("ownerId", "replyToId", "Initial text");

		// Act & Assert
		Assert.Throws<ArgumentException>(() => comment.Update(text));
	}

	[Fact]
	public void Update_WithNullText_ShouldThrowArgumentNullException()
	{
		// Arrange
		var comment = new Comment("ownerId", "replyToId", "Initial text");

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() => comment.Update(null));
	}
}
