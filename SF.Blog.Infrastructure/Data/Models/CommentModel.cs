using SF.Blog.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace SF.Blog.Infrastructure.Data.Models;
public class CommentModel : IComment, IDomainEntity
{
	public string Id { get; set; }
	public DateTime? Modified { get; set; }
	public DateTime Published { get; set; }
	public string Text { get; set; }

	public string ReplyToId { get; set; }
	public IEnumerable<CommentModel> Replies { get; set; }

	public string OwnerId { get; set; }
	public AppUserModel Owner { get; set; }
}
