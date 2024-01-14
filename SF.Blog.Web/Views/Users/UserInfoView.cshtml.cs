using SF.Blog.Infrastructure.Data.DTO;

namespace SF.Blog.Web.Views.Users;

public class UserInfoViewModel
{
	public string Name { get; set; }
	public string About { get; set; }
	public ICollection<PostDTO>? Posts { get; set; }

    public UserInfoViewModel(Core.User user, ICollection<PostDTO> posts)
    {
		Name = user.Name;
		About = user.About;
		Posts = posts;
    }
}
