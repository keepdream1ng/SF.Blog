using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SF.Blog.Infrastructure.Data.DTO;

namespace SF.Blog.Web.Views.Posts;

public class AllPostsViewModel
{
    public bool EditUI { get; set; }
	public ICollection<PostDTO> Posts { get; set; }

	public AllPostsViewModel(ICollection<PostDTO> posts, bool editUI = false)
    {
        EditUI = editUI;
        Posts = posts;
    }
}
