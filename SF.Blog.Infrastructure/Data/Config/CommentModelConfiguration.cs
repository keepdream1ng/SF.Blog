using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SF.Blog.Infrastructure.Data.Models;

namespace SF.Blog.Infrastructure.Data.Config;
public class CommentModelConfiguration : IEntityTypeConfiguration<CommentModel>
{
	public void Configure(EntityTypeBuilder<CommentModel> builder)
	{
		builder.ToTable("Comments")
			.HasKey(c => c.Id);

		builder.Property(c => c.Published)
			.IsRequired();

		builder.Property(c => c.Text)
			.IsRequired();

		builder.HasOne<AppUserModel>(c => c.Owner)
			.WithMany(u => u.Comments)
			.HasForeignKey(c => c.OwnerId)
			.IsRequired()
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasOne<PostModel>(c => c.ReplyTo)
			.WithMany(p => p.Comments)
			.HasForeignKey(c => c.ReplyToId)
			.IsRequired()
			.OnDelete(DeleteBehavior.Cascade);
	}
}
