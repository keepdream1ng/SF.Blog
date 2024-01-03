using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SF.Blog.Core;
using SF.Blog.Infrastructure.Data.Models;

namespace SF.Blog.Infrastructure.Data.Config;
public class PostModeConfiguration : IEntityTypeConfiguration<PostModel>
{
	public void Configure(EntityTypeBuilder<PostModel> builder)
	{
		builder.ToTable("Posts")
			.HasKey(p => p.Id);

		builder.Property(p => p.Title)
			.IsRequired();

		builder.Property(p => p.Content)
			.IsRequired();

		builder.Property(p => p.Published)
			.IsRequired();

		builder.HasOne<AppUserModel>(p => p.Owner)
			.WithMany(u => u.Posts)
			.HasForeignKey(p => p.OwnerId)
			.IsRequired()
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasMany<CommentModel>(p => p.Comments)
			.WithOne(c => c.ReplyTo)
			.OnDelete(DeleteBehavior.Restrict);
	}
}
