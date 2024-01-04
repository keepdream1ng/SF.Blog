using Ardalis.Specification;

namespace SF.Blog.Infrastructure.Data.Models.Specifications;
public class PostModelByIdSpec : SingleResultSpecification<PostModel>
{
    public PostModelByIdSpec(string id)
    {
        Query.Include(p => p.Tags)
            .ThenInclude(tp => tp.Tag)
            .Where(p => p.Id == id);
    }
}
