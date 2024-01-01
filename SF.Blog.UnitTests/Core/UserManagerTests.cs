using NSubstitute;

namespace SF.Blog.UnitTests.Core;
public class UserManagerTests
{
    private User CreateNewUser()
    {
        var user = new User("John Doe", "About me", new DateTime(1990, 1, 1));
        return user;
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateUserAndRepository()
    {
        // Arrange
        var user = CreateNewUser();
        var repositoryMock = Substitute.For<IUserWriteRepository>();
        repositoryMock.UpdateAsync(user).Returns(user);
        var userManager = new UserManager(user, repositoryMock);

        var newName = "Updated Name";
        var newAbout = "Updated About";
        var newDateOfBirth = new DateTime(2000, 1, 1);

        // Act
        var resultUser = await userManager.UpdateAsync(newName, newAbout, newDateOfBirth);

        // Assert
        Assert.Equal(newName, resultUser.Name);
        Assert.Equal(newAbout, resultUser.About);
        Assert.Equal(newDateOfBirth, resultUser.DateOfBirth);

        await repositoryMock.Received(1).UpdateAsync(Arg.Is<User>(u => u == resultUser));
    }

    [Fact]
    public async Task AddRoleAsync_ShouldAddRoleAndCallRepositoryUpdate()
    {
        // Arrange
        var user = CreateNewUser();
        var roleName = "Admin";
        var repositoryMock = Substitute.For<IUserWriteRepository>();
        var userManager = new UserManager(user, repositoryMock);
        repositoryMock.AddToRoleAsync(user, roleName).Returns(user);
        bool expected = true;

        // Act
        bool actual = await userManager.AddRoleAsync(roleName);

        // Assert
        Assert.Equal(expected, actual);
        Assert.Contains(user.Roles, role => role.Name == roleName);
        await repositoryMock.Received(1).AddToRoleAsync(Arg.Is<User>(u => u == user), Arg.Is<string>(str => str == roleName));
    }

    [Fact]
    public async Task RemoveRoleAsync_ShouldRemoveRoleAndCallRepositoryUpdate()
    {
        // Arrange
        var user = CreateNewUser();
        var repositoryMock = Substitute.For<IUserWriteRepository>();
        var userManager = new UserManager(user, repositoryMock);

        var roleName = "Admin";
        var role = new Role(roleName);
        user.AddRole(roleName);
        repositoryMock.RemoveFromRoleAsync(user, role).Returns(user);
        bool expected = true;

        // Act
        bool actual = await userManager.RemoveRoleAsync(role);

        // Assert
        Assert.Equal(expected, actual);
        Assert.DoesNotContain(user.Roles, r => r.Name == roleName);

        await repositoryMock.Received(1).RemoveFromRoleAsync(Arg.Is<User>(u => u == user), Arg.Is<Role>(role => role.Name == roleName));
    }

    [Fact]
    public async Task RemoveRoleAsync_ShouldReturnFalse_If_RoleIsNotPresent_And_NotCallRepositoryUpdate()
    {
        // Arrange
        var user = CreateNewUser();
        var repositoryMock = Substitute.For<IUserWriteRepository>();
        var userManager = new UserManager(user, repositoryMock);

        var roleName = "Admin";
        var role = new Role(roleName);
        repositoryMock.RemoveFromRoleAsync(user, role).Returns(user);
        bool expected = false;

        // Act
        bool actual = await userManager.RemoveRoleAsync(role);

        // Assert
        Assert.Equal(expected, actual);
        Assert.DoesNotContain(user.Roles, r => r.Name == roleName);
        await repositoryMock.DidNotReceive().RemoveFromRoleAsync(Arg.Is<User>(u => u == user), Arg.Is<Role>(role => role.Name == roleName));
    }

    [Fact]
    public async Task DeleteAsync_ShouldCallRepositoryDeleteAndReturnTrue()
    {
        // Arrange
        var user = CreateNewUser();
        var repositoryMock = Substitute.For<IUserWriteRepository>();
        var userManager = new UserManager(user, repositoryMock);

        // Act
        var result = await userManager.DeleteAsync();

        // Assert
        await repositoryMock.Received(1).DeleteAsync(Arg.Is<User>(u => u == user));
        Assert.True(result);
    }
}
