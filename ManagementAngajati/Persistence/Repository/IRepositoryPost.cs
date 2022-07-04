using ManagementAngajati.Models;

namespace ManagementAngajati.Persistence.Repository

{
    public interface IRepositoryPost : IRepository<long, Post>
    {
        public Task<List<Post>> GetPostsByIds(List<long> ids);
    }
}
