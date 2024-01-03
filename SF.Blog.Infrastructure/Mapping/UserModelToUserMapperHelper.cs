using SF.Blog.Core;
using SF.Blog.Infrastructure.Data.Models;

namespace SF.Blog.Infrastructure.Mapping;
/// <summary>
/// Class incapsulates logic to mapping UserModel to User with separate List of roles.
/// </summary>
public struct UserModelToUserMapperHelper : IUser
{
	public string Id { get; set; }
	public string Name { get; set; }
	public string About { get; set; }
	public DateTime DateOfBirth { get; set; }
	internal HashSet<Role> _roles;

    public UserModelToUserMapperHelper(AppUserModel userModel, IList<string> roles)
    {
		Id = userModel.Id;
		Name = userModel.Name;
		About = userModel.About;
		DateOfBirth = userModel.DateOfBirth;
		_roles = roles.Select(role => new Role(role)).ToHashSet();
    }
}
