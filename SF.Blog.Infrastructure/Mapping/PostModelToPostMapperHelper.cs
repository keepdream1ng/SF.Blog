using SF.Blog.Core;
using SF.Blog.Infrastructure.Data.Models;

namespace SF.Blog.Infrastructure.Mapping;

/// <summary>
/// Class incapsulates logic to mapping PostModel to Post with many to many relationship.
/// </summary>
public struct PostModelToPostMapperHelper : IPost
{
	public string Id { get; set; }
	public string OwnerId { get; set; }
	public string Title { get; set; }
	public string Content { get; set; }
	public DateTime Published { get; set; }
	public DateTime? Modified { get; set; }

	internal HashSet<Tag> _tags;

	public PostModelToPostMapperHelper(PostModel postModel)
	{
		Id = postModel.Id;
		OwnerId = postModel.OwnerId;
		Title = postModel.Title;
		Content = postModel.Content;
		Published = postModel.Published;
		Modified = postModel.Modified;
		_tags = postModel.Tags.Select(tp => new Tag(tp.Tag.Value)).ToHashSet();
	}
}
