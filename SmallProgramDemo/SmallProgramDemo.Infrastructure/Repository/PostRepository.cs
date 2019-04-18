using Microsoft.EntityFrameworkCore;
using SmallProgramDemo.Core.Entities;
using SmallProgramDemo.Core.Interface;
using SmallProgramDemo.Infrastructure.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallProgramDemo.Infrastructure.Repository
{
    public class PostRepository: IPostRepository
    {
        private readonly MyContext myContext;

        public PostRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }



        public void AddPost(Post post)
        {
            myContext.Posts.Add(post);
        }

        public async Task<PaginatedList<Post>> GetAllPosts(PostQueryParameters postQueryParameters)
        {
            var query = myContext.Posts.OrderBy(x => x.id);
            var count = await query.CountAsync();


            var data =  await query
                .Skip(postQueryParameters.PageIndex * postQueryParameters.PageSize)
                .Take(postQueryParameters.PageSize)
                .ToListAsync();

            return new PaginatedList<Post>(postQueryParameters.PageIndex, postQueryParameters.PageSize, count, data);
                
            //return await myContext.Posts.ToListAsync();
        }

        public async Task<Post> GetPostById(int id)
        {
            return await myContext.Posts.FindAsync(id);
        }
    }
}
