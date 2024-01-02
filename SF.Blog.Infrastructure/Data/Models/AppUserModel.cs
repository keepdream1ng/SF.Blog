using Microsoft.AspNetCore.Identity;
using SF.Blog.Core;
using System.ComponentModel.DataAnnotations;

namespace SF.Blog.Infrastructure.Data.Models;
public class AppUserModel : IdentityUser, IUser
{
	[Required]
	public string Name { get; set; }
	[Required]
	public DateTime DateOfBirth { get; set; }
	public string About { get; set; }
	public override string? UserName => base.Email;

	/// <summary>
	/// Navigation property for comments.
	/// </summary>
	public IEnumerable<CommentModel> Comments { get; set; }

	/// <summary>
	/// Navigation property for posts.
	/// </summary>
	public IEnumerable<Post> Posts { get; set; }
}
