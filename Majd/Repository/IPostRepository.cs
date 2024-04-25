using Majd.Models;

namespace Majd.Repository
{
    public interface IPostRepository
    {
        Task<Post> GetByIdAsync(string id);
        Task CreateAsync(Post post);
        Task DeleteAsync(string id);
    }
}
