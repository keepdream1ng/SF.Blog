using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace SF.Blog.Core;
public class Post : IDomainEntity, IPost
{
	public string Id { get; private set; }
	public string OwnerId { get; private set; }
	public string Title { get; private set; }
	public string Content { get; private set; }
	public DateTime Published { get; private set; }
	public DateTime? Modified { get; private set; }

	private HashSet<Tag> _tags;
	public IReadOnlyCollection<Tag> Tags => _tags;

	public Post(string ownerId, string title, string content)
	{
		Title = Guard.Against.NullOrWhiteSpace(title);
		Content = Guard.Against.NullOrWhiteSpace(content);
		OwnerId = Guard.Against.NullOrWhiteSpace(ownerId);
		Id = Guid.NewGuid().ToString();
		_tags = [];
		Published = DateTime.Now;
	}

	// Internal methods below are designed to work with domain level services.
	internal void Update(string title, string content)
	{
		Title = Guard.Against.NullOrWhiteSpace(title);
		Content = Guard.Against.NullOrWhiteSpace(content);
		Modified = DateTime.Now;
	}

	internal bool AddTag(string tag)
	{
		Guard.Against.NullOrWhiteSpace(tag);
		Guard.Against.InvalidFormat(tag, nameof(tag), @"^.{2,70}$", "Tag should be from 2 to 70 chars long.");
		var newTag = new Tag(tag);
		return _tags.Add(newTag);
	}

	internal bool RemoveTag(Tag tag)
	{
		Guard.Against.Null(tag);
		return _tags.Remove(tag);
	}
}
