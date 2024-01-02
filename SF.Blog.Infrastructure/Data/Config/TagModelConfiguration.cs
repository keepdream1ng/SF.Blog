using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SF.Blog.Core;
using SF.Blog.Infrastructure.Data.Models;

namespace SF.Blog.Infrastructure.Data.Config;
public class TagModelConfiguration : IEntityTypeConfiguration<TagModel>
{
	public void Configure(EntityTypeBuilder<TagModel> builder)
	{
		builder.ToTable("Tags")
			.HasKey(t => t.Id);

		builder.Property(t => t.Id)
			.ValueGeneratedOnAdd();

		builder.Property(t => t.Value)
			.IsRequired();

		builder.HasMany<PostModel>(t => t.Posts)
			.WithMany(p => p.Tags)
			.UsingEntity(
				l => l.HasOne(typeof(TagModel)).WithMany().OnDelete(DeleteBehavior.Restrict),
				r => r.HasOne(typeof(PostModel)).WithMany().OnDelete(DeleteBehavior.Restrict)
			);
	}
}
