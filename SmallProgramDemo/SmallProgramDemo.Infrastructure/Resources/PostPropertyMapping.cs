using SmallProgramDemo.Core.Entities;
using SmallProgramDemo.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallProgramDemo.Infrastructure.Resources
{
    /// <summary>
    /// Post的ResourceModel与EntityModeled的属性映射排序的对应类
    /// 将PostResource和Post作为父类的泛型
    /// </summary>
    public class PostPropertyMapping : PropertyMapping<PostResource, Post>
    {
        /// <summary>
        /// 执行父类的构造函数，并添加自定义的PostResource属性到Post属性集合的属性映射
        /// </summary>
        public PostPropertyMapping() : base(
            new Dictionary<string, List<MappedProperty>>
                (StringComparer.OrdinalIgnoreCase)  //不区分大小写的StringComparaer
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
