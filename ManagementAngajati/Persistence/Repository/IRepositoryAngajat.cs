
using System.Data;
using ManagementAngajati.Models;


namespace ManagementAngajati.Persistence.Repository
{
    public interface IRepositoryAngajat : IRepository<long, Angajat>
    {
        Task<Angajat> GetAngajatByUsernamePassword(string username, string password);

    }
}