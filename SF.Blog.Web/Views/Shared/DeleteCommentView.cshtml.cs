using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SF.Blog.Infrastructure.Data.DTO;

namespace SF.Blog.Web.Views.Shared;

public class DeleteCommentViewModel
{
	public string Id { get; set; }
	public string PostId { get; set; }

    public DeleteCommentViewModel() { }
    public DeleteCommentViewModel(CommentDTO comment)
    {
        Id = comment.Id;
        PostId = comment.PostId;
    }
}
