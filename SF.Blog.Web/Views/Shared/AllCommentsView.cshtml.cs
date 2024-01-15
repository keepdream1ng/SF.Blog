using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SF.Blog.Infrastructure.Data.DTO;

namespace SF.Blog.Web.Views.Shared;

public class AllCommentsViewModel
{
	public ICollection<CommentDTO>? Comments { get; set; }
	public bool EditUI { get; set; }
    public AllCommentsViewModel(ICollection<CommentDTO> comments, bool editUi = false)
    {
        Comments = comments;
        EditUI = editUi;
    }
}
