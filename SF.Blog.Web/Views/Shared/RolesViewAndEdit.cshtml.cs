using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SF.Blog.Web.Views.Shared;

public class RolesViewAndEditModel 
{
	public string Id { get; set; }
	public string Roles { get; set; }

    public RolesViewAndEditModel() { }

    public RolesViewAndEditModel(string id, string roles)
	{
		Id = id;
		Roles = roles;
	}
}
