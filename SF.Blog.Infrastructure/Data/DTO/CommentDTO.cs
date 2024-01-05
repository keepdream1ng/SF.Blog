namespace SF.Blog.Infrastructure.Data.DTO;
public record CommentDTO(string Id, string PostId, string AuthorsName, string Content);
