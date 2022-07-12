
using System.Data;
using ManagementAngajati.Models.AngajatModel;

namespace ManagementAngajati.Persistence.Repository
{
    public interface IRepositoryAngajat : IRepository<long, Angajat>
    {
        Task<Angajat> GetAngajatByUsername(string username);

    }
}