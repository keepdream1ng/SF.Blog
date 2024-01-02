using System.Data;

namespace SF.Blog.Core;
public class Comment : IDomainEntity, IComment
{
	public string Id { get; private set; }
	public string OwnerId { get; private set; }
	public string ReplyToId { get; private set; }
	public DateTime Published { get; private set; }
	public DateTime? Modified { get; private set; }
	public string Text { get; private set; }

	public Comment(string ownerId, string replyToId, string text)
	{
		OwnerId = Guard.Against.NullOrWhiteSpace(ownerId);
		ReplyToId = Guard.Against.NullOrWhiteSpace(replyToId);
		Text = Guard.Against.NullOrWhiteSpace(text);
		Published = DateTime.Now;
	}

	// Internal methods below are designed to work with domain level services.

	internal void Update(string text)
	{
		Text = Guard.Against.NullOrWhiteSpace(text);
		Modified = DateTime.Now;
	}
}
