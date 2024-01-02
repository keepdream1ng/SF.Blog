namespace SF.Blog.Core;

public interface IUser
{
    string About { get; }
    DateTime DateOfBirth { get; }
    string Id { get; set; }
    string Name { get; }
    string OwnerId { get; }
    IReadOnlyCollection<Role> Roles { get; }
}