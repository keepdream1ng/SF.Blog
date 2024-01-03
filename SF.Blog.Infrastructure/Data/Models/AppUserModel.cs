using Microsoft.AspNetCore.Identity;
using SF.Blog.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SF.Blog.Infrastructure.Data.Models;
public class AppUserModel : IdentityUser, IUser, IDbModel
{
	[Required]
	public string Name { get; set; }
	[Required]
	public DateTime DateOfBirth { get; set; }
	public string About { get; set; }
	public override string? UserName => base.Email;

	[InverseProperty(nameof(CommentModel.Owner))]
	public IEnumerable<CommentModel> Comments { get; set; }

	[InverseProperty(nameof(PostModel.Owner))]
	public IEnumerable<PostModel> Posts { get; set; }
}
