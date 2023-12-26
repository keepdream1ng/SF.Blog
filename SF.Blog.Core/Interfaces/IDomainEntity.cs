namespace SF.Blog.Core;
public interface IDomainEntity
{
	string Id { get; }
	string OwnerId { get; }
}
