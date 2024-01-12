using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SF.Blog.Infrastructure.Data.DTO;
using System.ComponentModel.DataAnnotations;

namespace SF.Blog.Web.Views.Posts;

public class EditPostViewModel 
{
	public string PostId { get; set; }

	[Required]
	[Display(Name = "Title")]
	[StringLength(60, ErrorMessage = "Field {0} should have minimum {2} and max {1} characters.", MinimumLength = 2)]
	public string Title { get; set; }

	[Required]
	[Display(Name = "Content")]
	[StringLength(2000, ErrorMessage = "Field {0} should have minimum {2} and max {1} characters.", MinimumLength = 5)]
	public string Content { get; set; }
	public string Tags { get; set; }

	public EditPostViewModel() { }
    public EditPostViewModel(PostDTO post) 
    {
		PostId = post.Id;
		Title = post.Title;
		Content = post.Content;
		Tags = post.Tags;
    }
}
