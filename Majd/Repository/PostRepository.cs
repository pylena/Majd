using Majd.Data;
using Majd.Models;

namespace Majd.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDBContext _db;

        public PostRepository(AppDBContext db)
        {
               _db = db;    
        }

       public async Task CreateAsync(Post post)
       {
            await _db.Posts.AddAsync(post);
            await _db.SaveChangesAsync();
           
       }

       public async  Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

       public async Task<Post> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
