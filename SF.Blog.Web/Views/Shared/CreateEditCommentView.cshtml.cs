using Microsoft.AspNetCore.Mvc.RazorPages;
using SF.Blog.Core;
using SF.Blog.Infrastructure.Data.DTO;
using System.ComponentModel.DataAnnotations;

namespace SF.Blog.Web.Views.Shared;

public class CreateEditCommentViewModel
{
	public string Id { get; set; }

	[Required]
	[Display(Name = "Content")]
	[StringLength(1000, ErrorMessage = "Field {0} should have minimum {2} and max {1} characters.", MinimumLength = 2)]
	public string CommentText { get; set; }

	public bool NewComment { get; set; } = true;

	public CreateEditCommentViewModel() { }
    public CreateEditCommentViewModel(CommentDTO comment)
    {
		NewComment = false;
		Id = comment.Id;
		CommentText = comment.Content;
    }
}
