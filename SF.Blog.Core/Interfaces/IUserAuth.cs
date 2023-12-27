using System.Collections.ObjectModel;

namespace SF.Blog.Core;
public interface IUserAuth
{
	string Id { get; set; }
	IReadOnlyCollection<Role> Roles { get; }
}