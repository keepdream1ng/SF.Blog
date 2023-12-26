namespace SF.Blog.UnitTests.Core;
public class PostTests
{
	private Post CreateNewPost()
	{
		return new Post("1", "Test", "Tests are important!");
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
