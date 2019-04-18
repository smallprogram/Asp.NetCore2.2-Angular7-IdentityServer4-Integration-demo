using Microsoft.EntityFrameworkCore;
using SmallProgramDemo.Core.Entities;
using SmallProgramDemo.Core.Interface;
using SmallProgramDemo.Infrastructure.Database;
using SmallProgramDemo.Infrastructure.Extensions;
using SmallProgramDemo.Infrastructure.Resources;
using SmallProgramDemo.Infrastructure.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallProgramDemo.Infrastructure.Repository
{
    public class PostRepository: IPostRepository
    {
        private readonly MyContext myContext;
        private readonly IPropertyMappingContainer propertyMappingContainer;

        public PostRepository(
            MyContext myContext,
            IPropertyMappingContainer propertyMappingContainer)
        {
            this.myContext = myContext;
            this.propertyMappingContainer = propertyMappingContainer;
        }



        public void AddPost(Post post)
        {
            myContext.Posts.Add(post);
        }

        public async Task<PaginatedList<Post>> GetAllPosts(PostQueryParameters postQueryParameters)
        {
            var query = myContext.Posts.AsQueryable();

            #region 根据需求 过滤相关字段
            if (!string.IsNullOrEmpty(postQueryParameters.Title.Trim()))
            {
                var title = postQueryParameters.Title.Trim().ToLowerInvariant();
                //直接检索
                //query = query.Where(x => x.Title.ToLowerInvariant() == title);
                //模糊查询
                query = query.Where(x => x.Title.ToLowerInvariant().Contains(title));

                //将pageindex设置为第一页起始，防止出现检索的数据不显示的问题
                //需要考虑一下怎么解决
                //postQueryParameters.PageIndex = 0;

            }
            #endregion

            //query = query.OrderBy(x => x.id);


            query = query.ApplySort(postQueryParameters.OrderBy, propertyMappingContainer.Resolve<PostResource, Post>());


            var count = await query.CountAsync();

            

            var data =  await query
                .Skip(postQueryParameters.PageIndex * postQueryParameters.PageSize)
                .Take(postQueryParameters.PageSize)
                .ToListAsync();

            var paginatedList = new PaginatedList<Post>(postQueryParameters.PageIndex, postQueryParameters.PageSize, count, data);


            return paginatedList;

            //return await myContext.Posts.ToListAsync();
        }

        public async Task<Post> GetPostById(int id)
        {
            return await myContext.Posts.FindAsync(id);
        }
    }
}
