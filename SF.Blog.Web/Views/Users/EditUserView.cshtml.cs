using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SF.Blog.Core;
using System.ComponentModel.DataAnnotations;

namespace SF.Blog.Web.Views.Users;
public class EditUserViewModel
{
	public string Id { get; set; }

	[Required]
	[Display(Name = "Name")]
	[Length(2, 25)]
	public string Name { get; set; }

	[Required]
	[Display(Name = "About")]
	[Length(2, 90)]
	public string About { get; set; }

	[Required]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
	[Display(Name = "Date of birth")]
	public DateTime DateOfBirth { get; set; } = DateTime.Now;
	// Thanks to setting initial value it passes current and not 01/01/0001 date in the browser input.

	[BindNever]
	public string? Roles { get; set; }

	public EditUserViewModel() { }
	public EditUserViewModel(Core.User user)
	{
		Id = user.Id;
		Name = user.Name;
		About = user.About;
		DateOfBirth = user.DateOfBirth;
		Roles = String.Empty;
		if (user.Roles.Count > 0)
		{
			string[] roles = user.Roles.Select(x => x.Name).ToArray();
			Roles = String.Join(" ", roles);
		}
	}
}
