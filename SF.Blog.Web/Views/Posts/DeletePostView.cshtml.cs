using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SF.Blog.Web.Views.Posts;

public class DeletePostViewModel
{
	public string Id { get; set; }
	public string Title { get; set; }

    public DeletePostViewModel() { }
    public DeletePostViewModel(string id, string title)
	{
		Id = id;
		Title = title;
	}
}
