using System.Collections.ObjectModel;

namespace SF.Blog.Core;
public class User : IDomainEntity, IUserAuth
{
	public string Id { get; set; }
	public string OwnerId => Id;
	public string Name { get; private set; }
	public string About { get; private set; }
	public DateTime DateOfBirth { get; private set; }
	internal HashSet<Role> _roles;
	public IReadOnlyCollection<Role> Roles => _roles;

    public User(string name, string about, DateTime dateOfBirth, string id = null)
    {
		CheckAndSetUserProperties(name, about, dateOfBirth);
		Id = string.IsNullOrWhiteSpace(id)? Guid.NewGuid().ToString() : id;
		_roles = [new Role("User")];
    }

	// Internal methods below are designed to work with domain level services.
	internal void Update(string name, string about, DateTime dateOfBirth)
	{
		CheckAndSetUserProperties(name, about, dateOfBirth);
	}

	internal bool AddRole(string role)
	{
		Guard.Against.NullOrWhiteSpace(role);
		return _roles.Add(new Role(role));
	}
	internal bool RemoveRole(Role role)
	{
		Guard.Against.Null(role);
		return _roles.Remove(role);
	}

	private void CheckAndSetUserProperties(string name, string about, DateTime dateOfBirth)
	{
		this.Name = Guard.Against.NullOrWhiteSpace(name);
		this.About = Guard.Against.NullOrWhiteSpace(about);
		this.DateOfBirth = Guard.Against.NullOrOutOfRange<DateTime>(dateOfBirth,
			nameof(dateOfBirth),
			DateTime.Today.AddYears(-120),
			DateTime.Today.AddYears(-5),
			"Date of birth should be realistic");
	}
}
