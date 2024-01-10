using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SF.Blog.Infrastructure.Data.DTO;

namespace SF.Blog.Web.Views.Users;

public class GetAllUsersViewModel 
{
	public ICollection<UserDTO> Users { get; set; }
}
