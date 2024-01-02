namespace SF.Blog.Core;

public interface IComment
{
	string Id { get; }
	DateTime? Modified { get; }
	string OwnerId { get; }
	DateTime Published { get; }
	string ReplyToId { get; }
	string Text { get; }
}