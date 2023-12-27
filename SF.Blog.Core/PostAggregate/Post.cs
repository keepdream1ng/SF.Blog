using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace SF.Blog.Core;
public class Post : IDomainEntity
{
	public string Id { get; private set; }
	public string OwnerId { get; private set; }
	public string Title { get; private set; }
	public string Content { get; private set; }
	public DateTime Published { get; private set; }
	public DateTime? Modified { get; private set; }

	private List<Tag> _tags;
	public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();

    public Post(string ownerId, string title, string content)
    {
		Title = Guard.Against.NullOrWhiteSpace(title);
		Content = Guard.Against.NullOrWhiteSpace(content);
		OwnerId = Guard.Against.NullOrWhiteSpace(ownerId);
		Id = Guid.NewGuid().ToString();
		_tags = new List<Tag>();
		Published = DateTime.Now;
    }

	// Internal methods below designed to work with domain level services.
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
		if (_tags.Any(t => t.Value == tag)) return false;
		var newTag = new Tag(tag);
		_tags.Add(newTag);
		return true;
	}

	internal bool RemoveTag(Tag tag)
	{
		Guard.Against.Null(tag);
		return _tags.Remove(tag);
	}
}
