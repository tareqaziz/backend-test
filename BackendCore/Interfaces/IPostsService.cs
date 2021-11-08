using BackendCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendCore.Interfaces
{
    public interface IPostsService
    {
        Task<IEnumerable<PostsDto>> GetTopPosts(int top);
    }
}