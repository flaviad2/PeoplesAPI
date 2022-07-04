using ManagementAngajati.Models;
using ManagementAngajati.Persistence.DbUtils;

namespace ManagementAngajati.Persistence.Repository
{
    public class RepositoryIstoricAngajat : IRepositoryIstoricAngajat
    {
        private readonly ManagementAngajatiContext _context; 
        
        public RepositoryIstoricAngajat(ManagementAngajatiContext context)
        {
            _context = context;
        }

        public RepositoryIstoricAngajat()
        {

        }
        public ManagementAngajatiContext GetManagementAngajatiContext()
        {
            return _context;
        }

        //eroare ridicata de repo daca mai exista cu acelasi angajat si post
        //exceptii suplimentare sa existe obiectele la care fac ref
        public Task<IstoricAngajat> Add(IstoricAngajat entity)
        {
            _context.IstoricuriAngajati.Add(entity);
            _context.SaveChanges();

            Task<IstoricAngajat> iTask = Task.FromResult(entity);
            return iTask;
        }

        public Task<IstoricAngajat> Delete(long id)
        {

            IstoricAngajat? deSters = _context.IstoricuriAngajati?.Find(id);

            if(deSters != null)
            {
                _context.IstoricuriAngajati.Remove(deSters);
                _context.SaveChanges();

                Task<IstoricAngajat> iTask = Task.FromResult(deSters);
                return iTask;
            }
            return null; 
        }

        public async Task<List<IstoricAngajat>> FindAll()
        {
            var dbIstoricuri = _context.IstoricuriAngajati.ToList();
            return dbIstoricuri;
        }

        public async Task<IstoricAngajat> FindOne(long id)
        {


            var dbIstoric = _context.IstoricuriAngajati.Where(i => i.ID == id).SingleOrDefault();
            if (dbIstoric != null)
            {
                var dbAngajat = _context.IstoricuriAngajati.Where(c => c.ID == id).Select(c => c.Angajat).SingleOrDefault();
                var dbPost = _context.IstoricuriAngajati.Where(c => c.ID == id).Select(c => c.Post).SingleOrDefault();
                dbIstoric.Angajat = dbAngajat;
                dbIstoric.Post = dbPost;
            }
            return dbIstoric;

        }

        public Task<IstoricAngajat> Update(IstoricAngajat entity, long id)
        {
            IstoricAngajat oldIstoric = _context.IstoricuriAngajati.Find(id);

            long idIstoric = oldIstoric.ID; 

            if(oldIstoric != null)
            {
                oldIstoric.ID = idIstoric;
                oldIstoric.Salariu = entity.Salariu;
                oldIstoric.Angajat = entity.Angajat;
                oldIstoric.DataReziliere = entity.DataReziliere;
                oldIstoric.DataAngajare = entity.DataAngajare;

                Task<IstoricAngajat> iTask = Task.FromResult(oldIstoric);
                return iTask; 
            }

            return null; 
        }

        public async Task<IstoricAngajat> FindOneByIdAngajat(long id)
        {
            var dbIstoric = _context.IstoricuriAngajati.Where(a => a.Angajat.ID == id).SingleOrDefault();
            return dbIstoric;
        }
    }
}
