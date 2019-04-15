using Microsoft.EntityFrameworkCore;
using SmallProgramDemo.Core.Entities;
using SmallProgramDemo.Core.Interface;
using SmallProgramDemo.Infrastructure.Database;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            return await myContext.Posts.ToListAsync();
        }
    }
}
