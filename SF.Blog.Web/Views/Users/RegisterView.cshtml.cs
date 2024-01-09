using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace SF.Blog.Web.Views.User;

public class RegisterViewModel
{
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

	[Required]
	[Display(Name = "Email")]
	[EmailAddress]
	public string EmailReg { get; set; }


	[Required]
	[DataType(DataType.Password)]
	[Display(Name = "Password")]
	[StringLength(100, ErrorMessage = "Field {0} should have minimum {2} and max {1} characters.", MinimumLength = 5)]
	public string PasswordReg { get; set; }

	[Required]
	[Compare("PasswordReg", ErrorMessage = "Passwords are not the same.")]
	[DataType(DataType.Password)]
	[Display(Name = "Password confirm")]
	public string PasswordConfirm { get; set; }
}
