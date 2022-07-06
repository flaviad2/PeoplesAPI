using System.Collections.Generic;
using ManagementAngajati.Models;
namespace ManagementAngajati.Persistence
{
    public interface IRepository<TId, T> where T : Entity<TId>
    {
        Task<T> Add(T entity);
        Task<T> Delete(TId id);
        Task<T> Update(T entity, TId i);
        Task<T> FindOne(TId id);
        Task<List<T>> FindAll();

    }
}