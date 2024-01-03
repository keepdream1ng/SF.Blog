using Ardalis.Specification;

namespace SF.Blog.Infrastructure.Data.Models.Specifications;
public class AppUserModelByIdSpec : SingleResultSpecification<AppUserModel>
{
    public AppUserModelByIdSpec(string id)
    {
        Query.Where(u => u.Id == id);
    }
}
