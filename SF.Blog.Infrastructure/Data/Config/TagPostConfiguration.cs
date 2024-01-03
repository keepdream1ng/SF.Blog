using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SF.Blog.Infrastructure.Data.Models;

namespace SF.Blog.Infrastructure.Data.Config;
public class TagPostConfiguration : IEntityTypeConfiguration<TagPost>
{
	public void Configure(EntityTypeBuilder<TagPost> builder)
	{
		builder.ToTable("TagPost")
			.HasKey(tp => new {tp.PostId, tp.TagId});

		builder.HasOne<TagModel>(tp => tp.Tag)
			.WithMany(t => t.Posts)
			.HasForeignKey(tp => tp.TagId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne<PostModel>(tp => tp.Post)
			.WithMany(p => p.Tags)
			.HasForeignKey(tp => tp.PostId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}
