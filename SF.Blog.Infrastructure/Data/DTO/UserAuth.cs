using SF.Blog.Core;

namespace SF.Blog.Infrastructure.Data.DTO;
public record UserAuth(string Id, IReadOnlyCollection<Role> Roles) : IUserAuth;
