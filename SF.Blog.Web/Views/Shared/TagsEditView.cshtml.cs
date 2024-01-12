using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SF.Blog.Web.Views.Shared;

public class TagsEditViewModel
{
	public string PostId { get; set; }
	public string Tags { get; set; }

    public TagsEditViewModel() { }
    public TagsEditViewModel(string postId, string tags)
    {
        PostId = postId;
        Tags = (tags is not null)? tags : string.Empty;
    }
}
