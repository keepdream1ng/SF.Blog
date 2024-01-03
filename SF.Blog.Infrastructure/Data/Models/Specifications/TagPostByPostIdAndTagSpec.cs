using Ardalis.Specification;

namespace SF.Blog.Infrastructure.Data.Models.Specifications;
public class TagPostByPostIdAndTagSpec : SingleResultSpecification<TagPost>
{
    public TagPostByPostIdAndTagSpec(string postId, string tagValue)
    {
        Query.Include(tp => tp.Tag).Where(tp => tp.PostId == postId && tp.Tag.Value == tagValue);
    }
}
