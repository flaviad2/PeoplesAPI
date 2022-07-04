
using ManagementAngajati.Models;
using System.Collections.Generic;

namespace ManagementAngajati.Persistence
{
    public interface IRepository<TId, T> where T : Entity<TId>
    {
        Task<T> Add(T entity);
        Task<T> Delete(TId id);
        Task<T> Update(T entity, TId id);
        Task<T> FindOne(TId id);
        Task<List<T>> FindAll();

    }
}