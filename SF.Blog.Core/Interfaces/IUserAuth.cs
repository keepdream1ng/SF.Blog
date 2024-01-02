using System.Collections.ObjectModel;

namespace SF.Blog.Core;
public interface IUserAuth
{
	string Id { get; }
	IReadOnlyCollection<Role> Roles { get; }
}