using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmallProgramDemo.Core.Entities;

namespace SmallProgramDemo.Infrastructure.Database.EntityConfiguration
{
    public class PostImageConfiguration : IEntityTypeConfiguration<PostImage>
    {
        public void Configure(EntityTypeBuilder<PostImage> builder)
        {
            builder.Property(x => x.FileName).IsRequired().HasMaxLength(100);
        }
    }
}
