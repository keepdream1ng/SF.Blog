using System.ComponentModel.DataAnnotations;

namespace SF.Blog.Web.Views.Posts;

public class CreatePostViewModel
{
	[Required]
	[Display(Name = "Title")]
	[StringLength(60, ErrorMessage = "Field {0} should have minimum {2} and max {1} characters.", MinimumLength = 2)]
	public string Title { get; set; }

	[Required]
	[Display(Name = "Content")]
	[StringLength(2000, ErrorMessage = "Field {0} should have minimum {2} and max {1} characters.", MinimumLength = 5)]
	public string Content { get; set; }
}
