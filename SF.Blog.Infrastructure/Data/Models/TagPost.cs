namespace SF.Blog.Infrastructure.Data.Models;
public class TagPost : IDbModel
{
	public int TagId {  get; set; }
	public TagModel Tag { get; set; }

	public string PostId { get; set; }
	public PostModel Post { get; set; }
}
