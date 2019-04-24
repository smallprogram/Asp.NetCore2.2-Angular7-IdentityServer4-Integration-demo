using SmallProgramDemo.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmallProgramDemo.Core.Interface
{
    public interface IPostRepository
    {
        Task<PaginatedList<Post>> GetAllPostsAsync(PostQueryParameters postQueryParameters);

        void AddPost(Post post);
        Task<Post> GetPostByIdAsync(int id);
        void Delete(Post post);
        void Update(Post post);
    }
}
