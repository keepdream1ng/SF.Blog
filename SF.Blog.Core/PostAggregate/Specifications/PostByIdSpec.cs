using Ardalis.Specification;

namespace SF.Blog.Core;
public class PostByIdSpec : Specification<Post>
{
    public PostByIdSpec(string Id)
    {
        Query.Where(post =>  post.Id == Id);
    }
}
