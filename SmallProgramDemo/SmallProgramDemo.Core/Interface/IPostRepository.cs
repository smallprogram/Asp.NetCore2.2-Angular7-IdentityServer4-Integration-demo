using SmallProgramDemo.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmallProgramDemo.Core.Interface
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPosts();

        void AddPost(Post post);
    }
}
