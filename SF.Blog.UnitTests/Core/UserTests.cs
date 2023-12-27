namespace SF.Blog.UnitTests.Core;
public class UserTests
{
    [Fact]
    public void Constructor_WithValidArguments_SetsPropertiesCorrectly()
    {
        // Arrange
        string name = "John Doe";
        string about = "Software Developer";
        DateTime dateOfBirth = new DateTime(1990, 1, 1);

        // Act
        var user = new User(name, about, dateOfBirth);

        // Assert
        Assert.NotNull(user.Id);
        Assert.Equal(name, user.Name);
        Assert.Equal(about, user.About);
        Assert.Equal(dateOfBirth, user.DateOfBirth);
        Assert.Single(user.Roles);
    }

    [Fact]
    public void Constructor_WithEmptyId_GeneratesUniqueId()
    {
        // Arrange
        string name = "John Doe";
        string about = "Software Developer";
        DateTime dateOfBirth = new DateTime(1990, 1, 1);

        // Act
        var user1 = new User(name, about, dateOfBirth);
        var user2 = new User(name, about, dateOfBirth);

        // Assert
        Assert.NotEqual(user1.Id, user2.Id);
    }

    [Fact]
    public void Update_WithValidArguments_UpdatesProperties()
    {
        // Arrange
        var user = new User("John", "Developer", new DateTime(1990, 1, 1));

        // Act
        user.Update("Jane", "Designer", new DateTime(1985, 5, 5));

        // Assert
        Assert.Equal("Jane", user.Name);
        Assert.Equal("Designer", user.About);
        Assert.Equal(new DateTime(1985, 5, 5), user.DateOfBirth);
    }

    [Theory]
    [InlineData("", "Developer", "1990-01-01")]
    [InlineData(" ", "Developer", "1990-01-01")]
    [InlineData("John", "", "1990-01-01")]
    [InlineData("John", " ", "1990-01-01")]
    public void Constructor_WithInvalidArguments_ThrowsException(string name, string about, string dateOfBirth)
    {
        // Arrange
        DateTime dob = DateTime.Parse(dateOfBirth);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new User(name, about, dob));
    }

    [Theory]
    [InlineData("John", "Developer", "1690-01-01")]
    [InlineData("John", "Developer", "2690-01-01")]
    public void Constructor_WithInvalid_Date_ThrowsException(string name, string about, string dateOfBirth)
    {
        // Arrange
        DateTime dob = DateTime.Parse(dateOfBirth);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new User(name, about, dob));
    }

    [Fact]
    public void AddRole_WithValidRole_AddsRole()
    {
        // Arrange
        var user = new User("John", "Developer", new DateTime(1990, 1, 1));

        // Act
        bool result = user.AddRole("Admin");

        // Assert
        Assert.True(result);
        Assert.Equal(2, user.Roles.Count);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void AddRole_WithInvalidRole_ThrowsException(string role)
    {
        // Arrange
        var user = new User("John", "Developer", new DateTime(1990, 1, 1));

        // Act & Assert
        Assert.Throws<ArgumentException>(() => user.AddRole(role));
    }

    [Fact]
    public void RemoveRole_WithValidRole_RemovesRole()
    {
        // Arrange
        var user = new User("John", "Developer", new DateTime(1990, 1, 1));
        var roleToRemove = user.Roles.First();

        // Act
        bool result = user.RemoveRole(roleToRemove);

        // Assert
        Assert.True(result);
        Assert.Empty(user.Roles);
    }

    [Fact]
    public void RemoveRole_WithInvalidRole_Returns_False()
    {
        // Arrange
        var user = new User("John", "Developer", new DateTime(1990, 1, 1));
        var invalidRole = new Role("InvalidRole");
        bool expected = false;

        // Act
        bool actual = user.RemoveRole(invalidRole);

        // Assert
        Assert.Equal(expected, actual);
    }
}
