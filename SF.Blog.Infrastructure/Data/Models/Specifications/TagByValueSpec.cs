using Ardalis.Specification;

namespace SF.Blog.Infrastructure.Data.Models.Specifications;
public class TagByValueSpec : SingleResultSpecification<TagModel>
{
    public TagByValueSpec(string value)
    {
        Query.Include(t => t.Posts).Where(t => t.Value == value);
    }
}
