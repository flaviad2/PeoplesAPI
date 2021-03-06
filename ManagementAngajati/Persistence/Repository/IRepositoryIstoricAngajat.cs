using ManagementAngajati.Models.IstoricAngajatModel;

namespace ManagementAngajati.Persistence.Repository
{
    public interface IRepositoryIstoricAngajat : IRepository<long, IstoricAngajat> 
    {
        Task<IstoricAngajat> FindOneByIdAngajat(long id);
    }
}
