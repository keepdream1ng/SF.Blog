using SF.Blog.Core;

namespace SF.Blog.Infrastructure.Data.Models;
public class PostModel : IPost
{
	public string Id { get; set;}
	public string Title { get; set;}
	public string Content { get; set;}
	public DateTime Published { get; set;}
	public DateTime? Modified { get; set;}

	public string OwnerId { get; set;}
	public AppUserModel Owner { get; set;}
	public IEnumerable<TagModel> Tags { get; set;}
	public IEnumerable<CommentModel> Comments { get; set;}
}
