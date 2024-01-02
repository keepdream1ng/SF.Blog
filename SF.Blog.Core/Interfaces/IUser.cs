namespace SF.Blog.Core;

public interface IUser
{
	string Id { get; }
	string Name { get; }
	string About { get; }
	DateTime DateOfBirth { get; }
}