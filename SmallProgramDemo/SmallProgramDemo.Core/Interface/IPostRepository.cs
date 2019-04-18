using SmallProgramDemo.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmallProgramDemo.Core.Interface
{
    public interface IPostRepository
    {
        Task<PaginatedList<Post>> GetAllPosts(PostQueryParameters postQueryParameters);

        void AddPost(Post post);
        Task<Post> GetPostById(int id);
    }
}
