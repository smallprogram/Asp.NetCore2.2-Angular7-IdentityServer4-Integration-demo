using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmallProgramDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallProgramDemo.Infrastructure.Database.EntityConfiguration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(x => x.Author).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Body).IsRequired().HasColumnType("nvarchar(max)");
        }
    }
}
