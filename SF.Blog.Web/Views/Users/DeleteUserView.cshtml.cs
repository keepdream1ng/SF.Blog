using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SF.Blog.Web.Views.Users;

public class DeleteUserViewModel 
{
	public string Id { get; set; }
	public string Name { get; set; }

    public DeleteUserViewModel() { }
    public DeleteUserViewModel(string id, string name)
	{
		Id = id;
		Name = name;
	}
}
