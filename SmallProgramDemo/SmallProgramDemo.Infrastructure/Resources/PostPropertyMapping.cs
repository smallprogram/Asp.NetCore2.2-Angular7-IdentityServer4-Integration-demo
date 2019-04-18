using SmallProgramDemo.Core.Entities;
using SmallProgramDemo.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallProgramDemo.Infrastructure.Resources
{
    public class PostPropertyMapping : PropertyMapping<PostResource, Post>
    {
        public PostPropertyMapping() : base(
            new Dictionary<string, List<MappedProperty>>
                (StringComparer.OrdinalIgnoreCase)
            {
                [nameof(PostResource.Title)] = new List<MappedProperty>
                    {
                        new MappedProperty{ Name = nameof(Post.Title), Revert = false}
                    },
                [nameof(PostResource.Body)] = new List<MappedProperty>
                    {
                        new MappedProperty{ Name = nameof(Post.Body), Revert = false}
                    },
                [nameof(PostResource.Author)] = new List<MappedProperty>
                    {
                        new MappedProperty{ Name = nameof(Post.Author), Revert = false}
                    }
            })
        {
        }
    }
}
