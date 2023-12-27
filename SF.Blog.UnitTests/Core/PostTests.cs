namespace SF.Blog.UnitTests.Core;
public class PostTests
{
	private Post CreateNewPost()
	{
		return new Post("1", "Test", "Tests are important!");
	}

    [Fact]
    public void Post_Constructor_Should_Set_Properties_Correctly()
    {
        // Arrange
        string ownerId = "user123";
        string title = "Test Title";
        string content = "Test Content";

        // Act
        var post = new Post(ownerId, title, content);

        // Assert
        Assert.NotNull(post.Id);
        Assert.Equal(ownerId, post.OwnerId);
        Assert.Equal(title, post.Title);
        Assert.Equal(content, post.Content);
        Assert.NotNull(post.Tags);
        Assert.Empty(post.Tags);
        Assert.True(post.Published <= DateTime.Now);
    }

    [Fact]
    public void Post_Update_Should_Update_Title_And_Content()
    {
        // Arrange
        var post = new Post("user123", "Test Title", "Test Content");
        string newTitle = "Updated Title";
        string newContent = "Updated Content";

        // Act
        post.Update(newTitle, newContent);

        // Assert
        Assert.Equal(newTitle, post.Title);
        Assert.Equal(newContent, post.Content);
        Assert.NotNull(post.Modified);
    }

	[Theory]
	[InlineData("", "Test", "content")]
	[InlineData(" ", "Test", "content")]
	[InlineData("1", "Test", "")]
	[InlineData("2", "", "content")]
	[InlineData("3", " ", "content")]
	[InlineData("4", "Test", " ")]
	public void PostShouldNotAllowEmptyInput(string mockOwner, string title, string content)
	{
		// Arrange.
		// Act.
		// Assert.
		Assert.Throws<ArgumentException>(() => new Post(mockOwner, title, content));
	}

	[Theory]
	[InlineData(null, "test title", "Lifechanging stuff here.")]
	[InlineData("1", null, "Lifechanging stuff here.")]
	[InlineData("2", "Test", null)]
	public void PostShouldNotAllowNullInput(string mockOwner, string title, string content)
	{
		// Arrange.
		// Act.
		// Assert.
		Assert.Throws<ArgumentNullException>(() => new Post(mockOwner, title, content));
	}

	[Theory]
	[InlineData(null, "Lifechanging stuff here.")]
	[InlineData("Test", null)]
	public void UpdateShouldNotAllowNullInput(string title, string content)
	{
		// Arrange.
		Post post = CreateNewPost();

		// Act.
		// Assert.
		Assert.Throws<ArgumentNullException>(() => post.Update(title, content));
	}

	[Theory]
	[InlineData("Test", "")]
	[InlineData("Test", " ")]
	[InlineData("", "content")]
	[InlineData(" ", "content")]
	public void UpdateShouldNotAllowEmptyInput(string title, string content)
	{
		// Arrange.
		Post post = CreateNewPost();

		// Act.
		// Assert.
		Assert.Throws<ArgumentException>(() => post.Update(title, content));
	}


	[Fact]
	public void AddTagShouldThrowIfNull()
	{
		// Arrange.
		Post post = CreateNewPost();
		string newTag = null;

		// Act.
		// Assert.
		Assert.Throws<ArgumentNullException>(() => post.AddTag(newTag));
	}

	[Fact]
	public void AddTagShouldThrowIfWhitespace()
	{
		// Arrange.
		Post post = CreateNewPost();
		string newTag = " ";

		// Act.
		// Assert.
		Assert.Throws<ArgumentException>(() => post.AddTag(newTag));
	}

	[Fact]
	public void AddTagShouldThrowIfInputIsLong()
	{
		// Arrange.
		Post post = CreateNewPost();
		string newTag = new string('x', 1000);

		// Act.
		// Assert.
		Assert.Throws<ArgumentException>(() => post.AddTag(newTag));
	}

	[Fact]
	public void AddTagShouldThrowIfInputIsShort()
	{
		// Arrange.
		Post post = CreateNewPost();
		string newTag = "t";

		// Act.
		// Assert.
		Assert.Throws<ArgumentException>(() => post.AddTag(newTag));
	}

	[Fact]
	public void AddTagShouldAddCorrectInput()
	{
		// Arrange.
		Post post = CreateNewPost();
		string newTag = "Tag example";
		bool expected = true;

		// Act.
		bool actual = post.AddTag(newTag);

		// Assert.
		Assert.True(post.Tags.Any(t => t.Value == newTag));
		Assert.Equal(expected, actual);
		Assert.Single(post.Tags);
	}

	[Fact]
	public void RemoveTagShouldDelete()
	{
		// Arrange.
		Post post = CreateNewPost();
		string newTag = "Tag example";
		post.AddTag(newTag);
		Tag tagToDelete = post.Tags.First();

		// Act.
		post.RemoveTag(tagToDelete);

		// Assert.
		Assert.False(post.Tags.Any(t => t.Value == newTag));
		Assert.Empty(post.Tags);
	}

	[Fact]
	public void RemoveTagShouldThrowIfNull()
	{
		// Arrange.
		Post post = CreateNewPost();
		Tag tagToDelete = null;

		// Act.
		// Assert.
		Assert.Throws<ArgumentNullException>(() => post.RemoveTag(tagToDelete));
	}

	[Fact]
	public void RemoveTagShouldReturnFalseIfTagNotFound()
	{
		// Arrange.
		Post post = CreateNewPost();
		Tag tagToDelete = new Tag("test Tag");
		bool expected = false;

		// Act.
		bool actual = post.RemoveTag(tagToDelete);

		// Assert.
		Assert.Equal(expected, actual);
	}
}
