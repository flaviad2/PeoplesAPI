using System.Data;
using ManagementAngajati.Models;
using ManagementAngajati.Persistence.DbUtils;

namespace ManagementAngajati.Persistence.Repository
{
    public class RepositoryAngajat : IRepositoryAngajat
    {

        private readonly ManagementAngajatiContext _context;

        public RepositoryAngajat(ManagementAngajatiContext context)
        {
            _context = context;
        }

       

        public ManagementAngajatiContext GetManagementAngajatiContext()
        {
            return _context;
        }

     

        public Task<Angajat> GetAngajatByUsernamePassword(string username, string password)
        {
            var found =  _context.Angajati.Where(a => a.Username == username && a.Password == password).FirstOrDefault();

            Task<Angajat> aTask = Task.FromResult(found);
            return aTask;

        }

       

        public Task<Angajat> Add(Angajat entity)
        {
            _context.Angajati.Add(entity);
            _context.SaveChanges();

            Task<Angajat> aTask = Task.FromResult(entity);
            return aTask;
        }

        public Task<Angajat> Delete(long id)
        {
            Angajat? deSters = _context.Angajati?.Find(id);

            if (deSters != null)
            {
                
                _context.Angajati.Remove(deSters);
                _context.SaveChanges();

                Task<Angajat> aTask = Task.FromResult(deSters);
                return aTask;

            }
            return null;
        }

        public Task<Angajat> Update(Angajat entity, long id)
        {
            Angajat oldAngajat = _context.Angajati.Find(id);
            long idAngajat = oldAngajat.ID; 

            if(oldAngajat!=null)
            {
                oldAngajat.ID = idAngajat;
                oldAngajat.Sex = entity.Sex;
                oldAngajat.Username = entity.Username;
                oldAngajat.Password = entity.Password;
                oldAngajat.Nume = entity.Nume;
                oldAngajat.Prenume = entity.Prenume;
                oldAngajat.DataNasterii = entity.DataNasterii;
                oldAngajat.Experienta = entity.Experienta;
                oldAngajat.Posturi = entity.Posturi;
                _context.Angajati.Update(oldAngajat);
                _context.SaveChanges();

                Task<Angajat> aTask = Task.FromResult(oldAngajat);
                return aTask;
            }
            return null;
        }

        public async Task<Angajat> FindOne(long id)
        {

            var dbAngajat = _context.Angajati.Where(a => a.ID == id).SingleOrDefault();
            var dbPosturiAngajat = _context.Angajati.Where(a => a.ID == id).SelectMany(c => c.Posturi).ToList();
            if (dbPosturiAngajat != null && dbAngajat!=null)
            {
                dbAngajat.Posturi = dbPosturiAngajat;
                return dbAngajat;

            }
            return null;
        }



        public async Task<List<Angajat>> FindAll()
        {

            var dbAngajati = _context.Angajati.ToList();
            for(int i=0; i<dbAngajati.Count; i++)
            {
                var dbPosturiAngajat = _context.Angajati.Where(a => a.ID == dbAngajati[i].ID).SelectMany(c => c.Posturi).ToList();
                dbAngajati[i].Posturi = dbPosturiAngajat;
            }
            return dbAngajati;
        }
    }
}
